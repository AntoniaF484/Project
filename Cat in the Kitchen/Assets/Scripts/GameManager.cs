using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

{
    private int score;
    public int lives;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button replayButton;

    private PlayerController playerController;
    private PathGenerator pathGenerator;
    
    private float distanceBetweenMinPath1;
    private float distanceBetweenMinPath2;
    
    private float distanceBetweenMaxPath1;
    private float distanceBetweenMaxPath2;

    
    
    private Vector3 playerStartPoint;

    public Transform GenerationPoint;
    private Vector3 path1StartPoint;
    

   public bool isGameActive;
    public Transform PathGenerator;
    private Vector3 pathStartPoint;
    public GameObject titleScreen;
 
    void Start()
    {
        UpdateLives(9);

    }

    public void StartGame (int difficulty)
    {
        UpdateScore(0);
        
        isGameActive = true;
        pathGenerator=FindObjectOfType<PathGenerator> ();
        
        
     pathGenerator.distanceBetweenMinPath1 *= difficulty;
     pathGenerator.distanceBetweenMaxPath1 *= difficulty;
      
     
     pathGenerator.distanceBetweenMinPath2 *= difficulty; 
     pathGenerator.distanceBetweenMaxPath2 *= difficulty;
        StartCoroutine(StartGeneratingPaths());
        titleScreen.gameObject.SetActive(false);
     
    }

   /* void Update()
    {
        StartGeneratingPaths();
    }
    */
   IEnumerator StartGeneratingPaths()
   {
   
       while (isGameActive) 
       {
           pathGenerator.GeneratePath1();
           pathGenerator.GeneratePath2();

           yield return new WaitForSeconds(0.5f);
       }
   }

   public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        
    }

    public void UpdateLives(int livesToTake)
    {
        lives += livesToTake;
        livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true);
        isGameActive = false;

    }

    public void RestartGame()
    {
        isGameActive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
