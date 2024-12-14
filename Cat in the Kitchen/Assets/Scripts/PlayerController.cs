using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    private DetectCollisions detectCollisions;

    //TEST
    public float jumpTime;
    private float jumpTimeCounter;
    public bool isHoldingJump;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = Vector3.down * 9.8f * gravityModifier;
        speedIncreaseCount = speedIncreasePosition;
        gameManager = FindObjectOfType<GameManager>();
        jumpTimeCounter = jumpTime;

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || jumpCount < maxJumps))
        {

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            jumpTimeCounter = jumpTime;
            isHoldingJump = true;
            if (isOnGround)
            {
                isOnGround = false;
            }


        }

        if (Input.GetKey(KeyCode.Space) && isHoldingJump && jumpTimeCounter > 0)
        {

            playerRb.AddForce(Vector3.up * jumpForce*Time.deltaTime, ForceMode.Impulse);
            jumpTimeCounter -= Time.deltaTime;

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
                jumpTimeCounter = 0;
            isHoldingJump = false;
            //TEST
            Physics.gravity = Vector3.down * 9.8f * gravityModifier*2;
        }

        if (isOnGround)
        {
            jumpTimeCounter = jumpTime;
            jumpCount = 0;
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

            isOnGround = true;


        }

    }
}





