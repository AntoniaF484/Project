
using System.Collections;
using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;


public class PlayerController : NetworkBehaviour
{

    private DetectCollisions detectCollisions;

    private Rigidbody playerRb;
    private float playerSize = 0.75f;
    public float jumpForce;
    public float moveSpeed = 20.0f;
    public float maxSpeed;
    public float acceleration = 2;
    public float speedIncreasePosition;
    private float speedIncreaseCount;
    public float maxAcceleration = 2;
    public bool isOnGround = true;
    public float gravityModifier;


    public int maxJumps = 2;
    public int jumpCount = 0;
    public GameManager gameManager;

    public float jumpTime;
    private float jumpTimeCounter;
    public bool isHoldingJump;


    public Transform groundCheck;
    public float groundCheckWidth;
    public LayerMask whatIsGround;

    private Collider myCollider;

    public bool enableMovement = false;


    //Audio 
    public AudioClip jumpSound;
    private AudioSource playerAudio;

// Authoritative player
    private NetworkVariable<Vector3> serverPosition = new(readPerm: NetworkVariableReadPermission.Everyone,
        writePerm: NetworkVariableWritePermission.Server);

    private NetworkVariable<Vector3> serverVelocity =
        new(readPerm: NetworkVariableReadPermission.Everyone,
            writePerm: NetworkVariableWritePermission.Server);

    private Vector2 input;
    private float inputSendInterval = 1f / 60f;
    private float inputSendTimer;

    private List<Snapshot> snapshots = new(); // snapshot used for interpolation on cleints


    [SerializeField] private Camera playerCamera;

    private float interpolationBackTime = 0.1f; // to smooth jitter in the client view, time to render far back


    // below are for client side to predict jump. client side view predicts the server will allow jump - makes view of jump more quick that waiting for server
    private int predictedJumpCount = 0;
    private bool predictedGrounded;
    private Vector3 predictedVelocity;
    private bool predictedHoldingJump;
    private float predictedJumpTimeCounter;
    private bool jumpStartedThisFrame;

    void Awake() // stabilize game framerate
    {
        Application.targetFrameRate = 60;
    }

    private struct Snapshot
    {
        public Vector3 pos;
        public float time;
        public Vector3 velocity;


        public Snapshot(Vector3 p, float t, Vector3 v) // snapshot of server state that will be sent to the client
        {
            pos = p;
            time = t;
            velocity = v;
        }

    }

    public override void OnNetworkSpawn()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (IsClient)
        {
            gameManager.allReady.OnValueChanged += OnAllReadyChanged;
        }

        Physics.gravity = Vector3.down * 9.8f * gravityModifier;
        if (IsServer) // server controls generating the paths and other objects, client just views what the server generates
        {
            PathGenerator generator = FindObjectOfType<PathGenerator>();
            
            if (generator != null)
            {
                generator.StartGeneration(); // Enable path generation on server
            }
        }

        if (IsClient) // here client recieves server updates
        {
            serverPosition.OnValueChanged += (oldVal, newVal) =>
            {

                snapshots.Add(new Snapshot(newVal, Time.time, serverVelocity.Value)); // store snapshot
                snapshots.Sort((a, b) => a.time.CompareTo(b.time)); // keep snapshots in order
               
               ReconcilePrediction(newVal); // used to fix discrepancies between client/server side

                while (snapshots.Count > 50)
                {
                    snapshots.RemoveAt(0);
                }
            };
        }
    }

    void ReconcilePrediction(Vector3 serverPos)
    {
        float error =
            Vector3.Distance(transform.position, serverPos); // distance between predicted pos and true server pos
        float correctionFactor = Mathf.Clamp01(error * 0.1f);
        if (error > 0.005f) // threshold, only correct if prediction is over this much off from true server pos
        {

            transform.position =
                Vector3.Lerp(transform.position, serverPos, correctionFactor); // smooth correction to avoid snapping

            predictedJumpCount = jumpCount; // reset to match server
            predictedGrounded = isOnGround;
        }
    }

    private void
        OnAllReadyChanged(bool oldValue,
            bool newValue) // once all players are ready, enable their movement after small wait
    {
        if (newValue)
        {
            StartCoroutine(EnableMovement(3f));
        }
    }

    void Start()
    {

        playerRb = GetComponent<Rigidbody>();
        if (!IsServer) // only server should control physics
        {
            playerRb.isKinematic = true;
        }

        speedIncreaseCount = speedIncreasePosition;
        jumpTimeCounter = jumpTime;
        playerAudio = GetComponent<AudioSource>();
        
    }

    private IEnumerator EnableMovement(float wait) // enable movement after wait
    {
        yield return new WaitForSeconds(wait);
        enableMovement = true;

    }

    void Update()
    {
        predictedGrounded =
            Physics.OverlapSphere(groundCheck.position, groundCheckWidth, whatIsGround).Length >
            0; // client view ground prediction
        if (!enableMovement) return;

        if (IsOwner) // below is client view prediction - it is not the actual position in the game, which is set by the server, but was used to attempt to minimise lag/jittering between the two
        {
       
            transform.position += predictedVelocity * Time.deltaTime;
            predictedVelocity.y += Physics.gravity.y * Time.deltaTime;
            if (predictedGrounded && predictedVelocity.y < 0)
            {
                predictedVelocity.y = 0;
            }
      
            inputSendTimer += Time.deltaTime;

            if (inputSendTimer >= inputSendInterval) // ground check sync
            {
                inputSendTimer = 0f;
                isOnGround = Physics.OverlapSphere(groundCheck.position, groundCheckWidth, whatIsGround).Length > 0;
            }

            bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
            bool jumpHeld = Input.GetKey(KeyCode.Space);
            bool jumpReleased = Input.GetKeyUp(KeyCode.Space);

            // client side appears to jump before server has allowed it - makes movement seem less lagged. Uses similar code to server jump (actual in game true position)
            if (jumpPressed && predictedJumpCount < maxJumps && predictedGrounded)
            {
                predictedJumpCount++; 
                predictedVelocity.y = jumpForce;
               predictedHoldingJump = true;
               predictedJumpTimeCounter = jumpTime;
               predictedGrounded = false;
               jumpStartedThisFrame = true;
           
            }
            if (jumpHeld && predictedHoldingJump && predictedJumpTimeCounter > 0f)
            {
                predictedVelocity.y += jumpForce * Time.deltaTime;
                predictedJumpTimeCounter -= Time.deltaTime;
            }
            
            if (jumpReleased)
            {
                predictedHoldingJump = false;
                predictedJumpTimeCounter = 0f;
            }
            
            SendInputServerRpc(jumpPressed, jumpHeld, jumpReleased); // send jump input to the server

        }



        if (!IsServer && snapshots.Count >= 2) // only clients other than the server need to interpolate

            if (IsOwner && jumpStartedThisFrame) // slows interpolation starting to make jump more visible for client
            {
                jumpStartedThisFrame = false; // reset flag
            }
            else
            {
                Interpolate();
            } 

    }


    void Interpolate()
    {
        if (snapshots.Count < 2) return; // need at least 2 pts to interpolate between
        float
            renderTime =
                Time.time -
                interpolationBackTime; // dont try to show real time, try to show slightly before
        Snapshot from = default;
        Snapshot to = default;


        for (int i = 0; i < snapshots.Count - 1; i++) // find snapshots around rendertime
        {
            if (snapshots[i].time <= renderTime && snapshots[i + 1].time >= renderTime)
            {
                from = snapshots[i];
                to = snapshots[i + 1];
                break;
            }
        }

        if (from.time == 0 || to.time == 0) return; // skip if no pair of snapshots found

        float duration = to.time - from.time; // how far between snapshots
        if (duration <= 0f) return; //if it takes no time, return

        float t = (renderTime - from.time) / duration; // how far progress between snapshots

        Vector3 predictedFrom =
            from.pos + from.velocity *
            (renderTime -
             from.time ); // predict where from should be now (if player keeps moving at the velocity) as player will keep moving 

        Vector3 predictedTo =
            to.pos + to.velocity * (renderTime - to.time); // predict where to should be now 

        transform.position = Vector3.Lerp(predictedFrom, predictedTo, t); // use above time corrected positions
        while (snapshots.Count > 2 &&
               snapshots[0].time <
               renderTime -
               interpolationBackTime * 2f) // keep at least 2 snapshots, but keep deleting based on oldst first
        {
            snapshots.RemoveAt(0); // remove old snapshots
        }

    }

    public Camera GetCamera()
    {
        return playerCamera;
    }
 
    [ServerRpc(RequireOwnership = false)]// Server is sent input and makes the ingame moves for the client's player
    void SendInputServerRpc(bool jumpPressed, bool jumpHeld, bool jumpReleased, ServerRpcParams rpcParams = default)
    {
        if (!enableMovement) return;

        isOnGround = Physics.OverlapSphere(groundCheck.position, groundCheckWidth, whatIsGround).Length > 0;

        if (transform.position.x > speedIncreaseCount)

        {
            speedIncreaseCount += speedIncreasePosition;
            moveSpeed *= acceleration;
        }
        else if (moveSpeed > maxSpeed)
        {
            moveSpeed = maxSpeed;
        }

        Vector3 velocity = playerRb.linearVelocity;
        velocity.x = moveSpeed;

        if (jumpPressed && (isOnGround || jumpCount < maxJumps))
        {
            velocity.y = jumpForce;
            jumpCount++;

            jumpTimeCounter = jumpTime; // resets jump time
            isHoldingJump = true;
            isOnGround = false;
            PlayJumpClientRpc();

        }

        playerRb.linearVelocity = velocity;

        if (jumpHeld && isHoldingJump &&
            jumpTimeCounter > 0) //If player is holding space, keep jumping until max jump time
        {

            playerRb.AddForce(Vector3.up * (jumpForce * Time.deltaTime), ForceMode.Impulse);
            jumpTimeCounter -= Time.deltaTime;

        }

        if (isOnGround)
        {
            jumpCount = 0;
        }

        if (jumpReleased) // when player release space, stop jumping and reset jumptime counter
        {
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z);
            jumpTimeCounter = 0;
            isHoldingJump = false;
        }

        serverPosition.Value = transform.position; // reflect transform position of server player for the client
        serverVelocity.Value = playerRb.linearVelocity; // reflect transform velocity of server player for the client

    }


    [ClientRpc] // Client hears own audio for jumping
    void PlayJumpClientRpc()
    {

        if (playerAudio != null && jumpSound != null)
            playerAudio.PlayOneShot(jumpSound, 1.0f);
    }

}





        








