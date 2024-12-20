using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelPlayerController : MonoBehaviour
{
    
    private Rigidbody playerRb;
    
    // player movement
    private float horizontalInput;
    private float verticalInput;
    public float turnSpeed;
    public float catPower;
// Bounds of game
    private int xRange = 40;
    private int zRange = 40;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime); //Turns the player on horizontal inputs
        
        verticalInput = Input.GetAxis("Vertical");
        Vector3 forward = transform.forward * verticalInput * catPower; // moves the player based on vertical input
        
        playerRb.velocity = forward;
        
        // all below keeps the player within the bounds of the game

        if (playerRb.transform.position.x > xRange)
        {
            playerRb.transform.position = new Vector3(xRange,playerRb.transform.position.y, playerRb.transform.position.z);
        }
        
        if (playerRb.transform.position.x < -xRange)
        {
            playerRb.transform.position = new Vector3(-xRange,playerRb.transform.position.y, playerRb.transform.position.z);
        }
        if (playerRb.transform.position.z > zRange)
        {
            playerRb.transform.position = new Vector3(playerRb.transform.position.x,playerRb.transform.position.y,zRange);
        }
        
        if (playerRb.transform.position.z < -zRange)
        {
            playerRb.transform.position = new Vector3(playerRb.transform.position.x,playerRb.transform.position.y,-zRange);
        }

    }
}
