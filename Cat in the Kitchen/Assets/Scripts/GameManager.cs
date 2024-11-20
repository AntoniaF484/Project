using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour

{
    private int score;

    public TextMeshProUGUI scoreText;
    //public Transform PathGenerator;
    //private Vector3 pathStartPoint;

   // public PlayerController Player;
 //   private Vector3 playerStartPoint;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0); 
        
        // pathStartPoint = PathGenerator.position;
        // playerStartPoint = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        
    }
}
