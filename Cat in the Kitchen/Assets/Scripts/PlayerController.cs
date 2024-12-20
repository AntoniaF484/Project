using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    private bool enableMovement = false;
   
    
    //Audio 
    public AudioClip jumpSound;
    private AudioSource playerAudio;

    
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = Vector3.down * 9.8f * gravityModifier;
        speedIncreaseCount = speedIncreasePosition;
        gameManager = FindObjectOfType<GameManager>();
        jumpTimeCounter = jumpTime;


        myCollider = GetComponent<Collider>();

        StartCoroutine(EnableMovement(3f)); //Wait to start moving the player 

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
        

        if (!enableMovement)
        {
            return;
        }


        isOnGround = Physics.OverlapSphere(groundCheck.position, groundCheckWidth, whatIsGround).Length>0;
            
            


        if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || jumpCount < maxJumps))//Player jumps if they are on the ground or have not jumped more than max.
        {

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //Force added to jump
            playerAudio.PlayOneShot(jumpSound,1.0f); //Audio on jump
          
            jumpCount++; // add a jump to the jump counter
            jumpTimeCounter = jumpTime; // resets jump time
            isHoldingJump = true;
            if (isOnGround)
            {
                isOnGround = false;
            }
        }
        
        

        if (Input.GetKey(KeyCode.Space) && isHoldingJump && jumpTimeCounter > 0)//If player is holding space, keep jumping until max jump time
        {

            playerRb.AddForce(Vector3.up * jumpForce*Time.deltaTime, ForceMode.Impulse);
            jumpTimeCounter -= Time.deltaTime;

        }

        if (Input.GetKeyUp(KeyCode.Space)) // when player release space, stop jumping and reset jumptime counter
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
                jumpTimeCounter = 0;
            isHoldingJump = false;
            
            Physics.gravity = Vector3.down * 9.8f * gravityModifier*2;
        }

        if (isOnGround)
        {
            jumpTimeCounter = jumpTime;
            jumpCount = 0;
        }

        if (isOnGround)
        {
           
            if (transform.position.x > speedIncreaseCount)
               
            {
              
                
                speedIncreaseCount += speedIncreasePosition;
                moveSpeed *= acceleration;
                
                
                
            }
            else if (moveSpeed > maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }


        if (gameManager.isGameActive)
        {
            playerRb.velocity = new Vector3(moveSpeed, playerRb.velocity.y, playerRb.velocity.z);
           
        }

        if (transform.position.y < playerSize)
        {
            transform.position = new Vector3(transform.position.x, playerSize, 0);
        }



    }
    
}





