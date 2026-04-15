using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
   
    public bool easyPath;
    public bool doublePoints;
   

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
        IndividualPlayerStats playerStats = other.GetComponentInParent<IndividualPlayerStats>();

        if (playerStats != null)
        {
            if (doublePoints)
            {
                powerUpManager.ActivateDoubleScore(playerStats, powerUpLength);
            }
            
        }
        
        else if (easyPath)
        {
            powerUpManager.ActivatePowerUp(easyPath, powerUpLength);
           
        }
        
        gameObject.SetActive(false);

       
        
    }
}