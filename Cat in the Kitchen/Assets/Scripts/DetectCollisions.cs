using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectCollisions : MonoBehaviour
{
    public int scoreValue;
    public int livesValue;
    private GameManager gameManager;
    public bool isOnGround = false;
   // public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == ("Player")) //when the player collides with an object, relevant scores added and object deactivates
        {
            gameManager.UpdateScore(scoreValue);
            gameManager.UpdateLives(livesValue);
            gameObject.SetActive(false);
        }
        
   
    }

   private void OnCollisionEnter(Collision collision)
    {
        
        if (gameManager.lives <= 0 || collision.gameObject.CompareTag("Ground") ) //Game Ober if the player lands on the ground or loses all lives
        {
            
           
            isOnGround = true;
            Debug.Log("Game Over!");
            gameManager.GameOver();
        
           
        }
    }
}




