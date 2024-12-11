using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private bool doublePoints;
    private bool extraLife;

    public bool powerUpActive;

    private float powerUpLengthCounter;

    private GameManager gameManager;
    private PathGenerator pathGenerator;
    private  DetectCollisions detectCollisions;

    private int normalScore;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType < GameManager>();
        detectCollisions = FindObjectOfType<DetectCollisions>();
        pathGenerator = FindObjectOfType<PathGenerator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpActive = true)
        {
            powerUpLengthCounter -= Time.deltaTime;

            if (doublePoints)
            {
                gameManager.powerUpActive = true;
            }

            if (powerUpLengthCounter <= 0)
            {
                gameManager.powerUpActive = false;
            }
        }
        
        
    }

    public void ActivatePowerUp(bool points, bool life, float time)
    {
        doublePoints = points;
        extraLife = life;
        powerUpLengthCounter = time;

        normalScore = gameManager.addedScore;
        powerUpActive = true;

    }
}
