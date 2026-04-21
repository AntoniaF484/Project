
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
        
        IndividualPlayerStats playerStats = other.GetComponentInParent<IndividualPlayerStats>();
        if (other.CompareTag("Player")) //when the player collides with an object, relevant scores added and object deactivates
        {
           
            if (playerStats != null)
            {
                playerStats.UpdateScore(scoreValue);
                playerStats.UpdateLives(livesValue);
            }

            gameObject.SetActive(false);
        }
        
   
    }

  private void OnCollisionEnter(Collision collision) // if player collides with the floor, they will instantly die
    {
        
        IndividualPlayerStats playerStats = GetComponentInParent<IndividualPlayerStats>();

        if (playerStats != null && playerStats.lives.Value <= 0 ||
            collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            playerStats.Dead(); 
            gameManager.PlayerDead(playerStats); 
            isOnFloor = true;
        }
        
    }
    
       
 

}




