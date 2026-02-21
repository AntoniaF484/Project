using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectCollisions : MonoBehaviour
{
    public int scoreValue;
    public int livesValue;
    private GameManager gameManager;
    public bool isOnFloor = false;
  

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
        
      
      if (gameManager.lives <= 0 ||collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
      {
         
          gameManager.GameOver();
          isOnFloor = true;
      } 
    }
       
 

}




