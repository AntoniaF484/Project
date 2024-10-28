using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
private Rigidbody playerRb;
public float jumpForce; 
public float horizontalInput;
//public float fallMultiplier = 2.5f;
//public float lowJumpMultiplier = 2.0f;
public float speed = 10.0f;
public bool isOnGround = true;
public float gravityModifier;
  
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    Physics.gravity*=gravityModifier;
    
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
      {
          playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }
      horizontalInput = Input.GetAxis("Horizontal");
      transform.Translate(Vector3.right*horizontalInput*Time.deltaTime*speed);
      
      if (transform.position.y <0.75)
      {
          transform.position = new Vector3(transform.position.x, 0.75f, 0);
      }
            
    }
//private void FixedUpdate(){

//if (playerRb.velocity.y<0){
//playerRb.velocity+=Vector3.up*Physics.gravity.y*(fallMultiplier-1)*Time.deltaTime;}
//else if(playerRb.velocity.y>0&&!Input.GetButton ("Jump")){
//playerRb.velocity+=Vector3.up*Physics.gravity.y*(lowJumpMultiplier)*Time.deltaTime;}}

private void OnCollisionEnter (Collision collision){
if (collision.gameObject.CompareTag("Ground")){
isOnGround=true;
//gameOver=true;
Debug.Log ("Game Over!");}}
}
