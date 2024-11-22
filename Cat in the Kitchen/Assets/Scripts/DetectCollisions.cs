using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectCollisions : MonoBehaviour
{
    public int scoreValue;
    private GameManager gameManager;
    public bool isOnGround = false;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == ("Player"))
        {
            gameManager.UpdateScore(scoreValue);
            gameObject.SetActive(false);
        }
        
   
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            gameOver = true;
            Debug.Log("Game Over!");
            gameManager.GameOver();
           
        }
    }
}




