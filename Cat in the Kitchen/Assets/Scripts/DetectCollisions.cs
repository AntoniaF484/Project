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

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == ("Player"))
        {
            gameManager.UpdateScore(scoreValue);
            gameManager.UpdateLives(livesValue);
            gameObject.SetActive(false);
        }
        
   
    }

   private void OnCollisionEnter(Collision collision)
    {
        
        if (gameManager.lives <= 0 || collision.gameObject.CompareTag("Ground") )
        {
            
            Debug.Log("Collision detected with: " + collision.gameObject.tag);
            Debug.Log("Current lives: " + gameManager.lives);
            isOnGround = true;
            Debug.Log("Game Over!");
            gameManager.GameOver();
        
           
        }
    }
}




