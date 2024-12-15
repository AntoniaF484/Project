using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI hiScoreText;
    public Button replayButton;
    public GameObject titleScreen;

    private PlayerController playerController;
    private PathGenerator pathGenerator;
    
    
    private int score;
    public int addedScore;
    public int lives;
    public int hiScore;
    
    public bool isGameActive;
    public Transform PathGenerator;
    
    //Distance Between Path Variables
    private float distanceBetweenMinPath1;
    private float distanceBetweenMinPath2;
    
    private float distanceBetweenMaxPath1;
    private float distanceBetweenMaxPath2;
    
    
   // PowerUps
   private PowerUpManager powerUpManager;
   public bool scorePowerUpActive;
   public bool extraLifePowerUpActive;
   
   // TEST
   //[SerializeField] HighScoreHandler highScoreHandler;
  // [SerializeField] string playerName;

  public int SecondPlace;
  public int ThirdPlace;
  public TextMeshProUGUI firstPlaceScore;
  private int firstScore;
 
    void Start()
    {
        UpdateLives(9);
        powerUpManager = FindObjectOfType<PowerUpManager>();

        if (PlayerPrefs.HasKey("HighScore") != null)
        {
            hiScore = PlayerPrefs.GetInt("HighScore");
        }

    }

    public void StartGame (int difficulty) // distance between generated paths increases with difficulty selected
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
   IEnumerator StartGeneratingPaths() //calls path generator while the game is active
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

      if (scorePowerUpActive)
      {
          scoreToAdd *= 2;
      }
       score += scoreToAdd;
        scoreText.text = "Score: " + score;
        if (score > hiScore)
        {
            hiScore = score;
            PlayerPrefs.SetInt("HighScore",hiScore);
        }
        
        UpdateLeaderBoard();

        hiScoreText.text = "High Score: " + hiScore;
        
   }

   public void UpdateLeaderBoard()
   {
       firstScore = hiScore;
       firstPlaceScore.text = firstScore.ToString();
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
