using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public bool doublePoints;
    public bool extraLife;

    public float powerUpLength;

    private PowerUpManager powerUpManager;
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
            powerUpManager.ActivatePowerUp(doublePoints, extraLife, powerUpLength);
        }
        
        gameObject.SetActive(false);
    }
}
