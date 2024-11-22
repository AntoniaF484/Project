using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float playerSize = 0.75f;
    public float jumpForce;
    public float horizontalInput;
    public float moveSpeed = 20.0f;
    public float maxSpeed;
    public float acceleration = 2;
    public float speedIncreasePosition;
    private float speedIncreaseCount;
    public float maxAcceleration = 2;
    public float maxXSpeed = 10.0f;
    public bool isOnGround = true;

    public float gravityModifier;

//public bool gameOver = false;
    public int maxJumps = 2;
    public int jumpCount = 0;
    public GameManager gameManager;
    private DetectCollisions detectCollisions;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
       // Physics.gravity *= gravityModifier;
       Physics.gravity = Vector3.down * 9.8f * gravityModifier; 
        speedIncreaseCount = speedIncreasePosition;

        // yield return null;
        //playerRb.velocity = new Vector3(moveSpeed, playerRb.velocity.y, playerRb.velocity.z);
        //playerRb.WakeUp();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps /*&& /*!gameOver*/)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }

        if (transform.position.x > speedIncreaseCount)
        {
            speedIncreaseCount += speedIncreasePosition;
            moveSpeed *= acceleration;
        }
        else if (moveSpeed > maxSpeed)
        {
            moveSpeed = maxSpeed;
        }

        //playerRb.velocity = new Vector3(moveSpeed, playerRb.velocity.y, playerRb.velocity.z);


        if (gameManager.isGameActive)
        {
            playerRb.velocity = new Vector3(moveSpeed, playerRb.velocity.y, playerRb.velocity.z);
        }

        if (transform.position.y < playerSize)
        {
            transform.position = new Vector3(transform.position.x, playerSize, 0);
        }



    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Path"))
        {
            jumpCount = 0;
        }
        
      
    }

}



