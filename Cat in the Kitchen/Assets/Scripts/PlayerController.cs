using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
private Rigidbody playerRb;
private float playerSize = 0.75f;
public float jumpForce; 
public float horizontalInput;
public float speed = 10.0f;
public bool isOnGround = true;
public float gravityModifier;
public bool gameOver = false;
  
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    Physics.gravity*=gravityModifier;
    
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
      {
          playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }
      horizontalInput = Input.GetAxis("Horizontal");
     transform.Translate(Vector3.right*horizontalInput*Time.deltaTime*speed);

      
      if (transform.position.y <playerSize)
      {
          transform.position = new Vector3(transform.position.x, playerSize, 0);
      }
            
    }
private void OnCollisionEnter (Collision collision){
if (collision.gameObject.CompareTag("Ground")){
isOnGround=true;
gameOver=true;
Debug.Log ("Game Over!");}}
}
