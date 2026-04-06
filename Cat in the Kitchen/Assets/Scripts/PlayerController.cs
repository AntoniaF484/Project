
using System.Collections;
using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;
using System.Linq;

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

    private Vector2 input;
    private float inputSendInterval = 1f / 60f;
    private float inputSendTimer;
    private Queue<Snapshot> snapshots = new();

    private struct Snapshot
    {
        public Vector3 pos;
        public float time;

        public Snapshot(Vector3 p, float t)
        {
            pos = p;
            time = t;
        }

    }

    public override void OnNetworkSpawn()
    {

        Physics.gravity = Vector3.down * 9.8f * gravityModifier;
        if (IsServer)
        {


            PathGenerator generator = FindObjectOfType<PathGenerator>();


            if (generator != null)
            {
                generator.StartGeneration(); // Enable path generation on server
            }
        }

        if (IsClient)
        {
            serverPosition.OnValueChanged += (oldVal, newVal) =>
            {
                snapshots.Enqueue(new Snapshot(newVal, Time.time));

                while (snapshots.Count > 5)
                {
                    snapshots.Dequeue();
                }
            };


        }

        StartCoroutine(EnableMovement(3f));
    }


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        speedIncreaseCount = speedIncreasePosition;
        // gameManager = FindObjectOfType<GameManager>();
        jumpTimeCounter = jumpTime;


        //myCollider = GetComponent<Collider>();

        // StartCoroutine(EnableMovement(3f)); //Wait to start moving the player 

        playerAudio = GetComponent<AudioSource>();


    }

    private IEnumerator EnableMovement(float wait)
    {
        yield return new WaitForSeconds(wait);
        enableMovement = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!enableMovement) return;

        if (IsOwner)
        {
            inputSendTimer += Time.deltaTime;

            if (inputSendTimer >= inputSendInterval)
            {
                inputSendTimer = 0f;
                isOnGround = Physics.OverlapSphere(groundCheck.position, groundCheckWidth, whatIsGround).Length > 0;
            }
            bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
            bool jumpHeld = Input.GetKeyDown(KeyCode.Space);
            SendInputServerRpc(jumpPressed, jumpHeld);
        }


        if (snapshots.Count >= 2)

            Interpolate();

    }

    void Interpolate()
    {
        if (snapshots.Count < 2) return;


        Snapshot from = snapshots.Peek();
        Snapshot to = snapshots.ElementAt(1);

        float duration = to.time - from.time;
        if (duration <= 0f) return;

        float elapsed = Time.time - from.time;
        float t = Mathf.Clamp01(elapsed / duration);

        transform.position = Vector3.Lerp(from.pos, to.pos, t);

        if (t >= 1f) snapshots.Dequeue();
    }





    [ServerRpc(RequireOwnership = false)]
    void SendInputServerRpc(bool jumpPressed, bool jumpHeld, ServerRpcParams rpcParams = default)
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

            playerRb.AddForce(Vector3.up * (jumpForce*Time.deltaTime), ForceMode.Impulse);
            jumpTimeCounter -= Time.deltaTime;

        }

        if (isOnGround)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyUp(KeyCode
                .Space)) // when player release space, stop jumping and reset jumptime counter
        {
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z);
            jumpTimeCounter = 0;
            isHoldingJump = false;
        }

        playerRb.linearVelocity = velocity;


        serverPosition.Value = transform.position;

    }





            


            [ClientRpc]
          void PlayJumpClientRpc()
            {

                if (playerAudio != null && jumpSound != null)
                    playerAudio.PlayOneShot(jumpSound, 1.0f);
            }


          



         /*     




               if (gameManager.isGameActive)
               {
                   playerRb.linearVelocity = new Vector3(moveSpeed, playerRb.linearVelocity.y, playerRb.linearVelocity.z);

               }

         */



        }





        








