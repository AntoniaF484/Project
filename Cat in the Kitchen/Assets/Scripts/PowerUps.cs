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

    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        powerUpManager = FindObjectOfType < PowerUpManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            powerUpManager.ActivatePowerUp(doublePoints, easyPath, extraLife, livesValue, powerUpLength);
        }
       
        gameObject.SetActive(false);
    }
}