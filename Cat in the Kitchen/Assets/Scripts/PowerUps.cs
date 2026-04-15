
using Unity.Netcode;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
   
    
    public bool doublePoints;
    public bool takeOtherLives;
   

    public float powerUpLength;

    private PowerUpManager powerUpManager;
    private BonusLevelPowerUpManager bonusLevelPowerUpManager;

    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        bonusLevelPowerUpManager = FindObjectOfType < BonusLevelPowerUpManager>(); 
        powerUpManager = FindObjectOfType < PowerUpManager>();
        gameManager = FindObjectOfType<GameManager>();
    }
    

    private void OnTriggerEnter(Collider other) //on collision with powerup, parameters are sent to power up manager and powerup is deactivated
    {
        if (!NetworkManager.Singleton.IsServer) return;
        IndividualPlayerStats playerStats = other.GetComponentInParent<IndividualPlayerStats>();

        if (playerStats != null)
        {
            if (doublePoints)
            {
                powerUpManager.ActivateDoubleScore(playerStats, powerUpLength);
            }

            if (takeOtherLives)
            {
                powerUpManager.ActivateTakeOtherLives(playerStats);
            }
            
        }
        
        gameObject.SetActive(false);

       
        
    }
}