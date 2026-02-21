using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
   
    private bool easyPath;
    

    public bool powerUpActive=false;

    private float powerUpLengthCounter; // time that powerup remains active for

    private GameManager gameManager;
    private PathGenerator pathGenerator;
    private  DetectCollisions detectCollisions;
   // private DifficultyButton difficultyButton;

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


        originalDistanceMin = pathGenerator.distanceBetweenMinPath1;
        originalDistanceMax = pathGenerator.distanceBetweenMaxPath1;




    }

    // Update is called once per frame
    void Update()
    {
        
       
        if (powerUpActive == true)
            
        {
            
            powerUpLengthCounter -= Time.deltaTime;
            
            

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
            
            pathGenerator.distanceBetweenMaxPath1 = originalDistanceMax;
            pathGenerator.distanceBetweenMinPath1 = originalDistanceMin;
            pathGenerator.maxYchangePath1 = 15;
        }
        
        
    }

    public void ActivatePowerUp(bool path, float time)
    {
        
        
       
        powerUpLengthCounter = time; 
        powerUpActive = true;
        
        
    }
}
