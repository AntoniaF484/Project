using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelDetectCollisions : MonoBehaviour
{

    public int scoreValueFromObj;
   
   // public int score;

   // public GameManager bonusGameManager;
   
   private BonusLevelGameManager bonusGameManager;

  // public GameManager bonusGameManager;

    // Start is called before the first frame update
    void Start()
    {
        bonusGameManager = FindObjectOfType<BonusLevelGameManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))//when the player collides with an object, relevant scores added and object deactivates
        {
            Destroy(gameObject);
            //score += scoreValueFromObj;
            bonusGameManager.UpdateScore(scoreValueFromObj);
        }
        
    }

   
}
