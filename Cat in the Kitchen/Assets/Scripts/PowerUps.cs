using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public bool doublePoints;
    public bool extraLife;
    public bool easyPath;
    public int livesValue;

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
        if (other.name == "Player" && gameManager. returnFromBonusLevel)
        {
            bonusLevelPowerUpManager.ActivatePowerUp(doublePoints, easyPath, extraLife, livesValue, powerUpLength);
            gameObject.SetActive(false);
        }
        
        else 
        {
            powerUpManager.ActivatePowerUp(doublePoints, easyPath, extraLife, livesValue, powerUpLength);
            gameObject.SetActive(false);
        }

       
        
    }
}