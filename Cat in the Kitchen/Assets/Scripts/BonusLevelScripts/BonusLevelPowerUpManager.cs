using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelPowerUpManager : MonoBehaviour
{
    private bool doublePoints;
    private bool extraLife;
    private bool easyPath;
    private int livesValue;

    public bool powerUpActive=false;

    private float powerUpLengthCounter; // time that powerup remains active for

    private GameManager gameManager;
    private PathGenerator pathGenerator;
    private  DetectCollisions detectCollisions;
    private DifficultyButton difficultyButton;

    private int normalScore;

    private float difficulty;
   
    //Min and max distance between platforms prior to hitting easypath powerup
   private float originalDistanceMax; 
   private float originalDistanceMin;
  
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType < GameManager>();
        pathGenerator = FindObjectOfType<PathGenerator>();
    
    }

    // Update is called once per frame
    void Update()
    {
        
       
        if (powerUpActive == true)
            
        {
            
            powerUpLengthCounter -= Time.deltaTime;

            if (doublePoints)
            {
                gameManager.scorePowerUpActive = true;
            }
            

           if (easyPath) 
            {
             pathGenerator.distanceBetweenMaxPath1 = 0;
               pathGenerator.distanceBetweenMinPath1 = 0;
                pathGenerator.maxYchangePath1 = 0;
            }
            
        }
        if (powerUpLengthCounter <= 0)
        {
            powerUpActive = false;
            
            pathGenerator.distanceBetweenMaxPath1 = 2;
            pathGenerator.distanceBetweenMinPath1 = 4;
            pathGenerator.maxYchangePath1 = 15;
        }
        
        
    }

    public void ActivatePowerUp(bool points, bool path, bool life, int livesValue, float time)
    {
        doublePoints = points;
        easyPath = path;
        extraLife = life;
       
        powerUpLengthCounter = time; 

        normalScore = gameManager.addedScore;
        powerUpActive = true;
        
        if (extraLife)
        {
            gameManager.UpdateLives(livesValue);
        }
        
    }
}