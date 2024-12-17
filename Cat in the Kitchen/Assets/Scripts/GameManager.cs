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
    public static int totalScore;
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




    public TextMeshProUGUI firstPlaceScore;
    private int firstScore;
    public TextMeshProUGUI secondPlaceScore;
    private int secondScore;
    public TextMeshProUGUI thirdPlaceScore;
    private int thirdScore;


    public bool returnFromBonusLevel;
   // private nextscene reachBonusLevel;
    public int autoDifficulty = 1;

    void Start()
    {

       
        
               UpdateLives(9);
            powerUpManager = FindObjectOfType<PowerUpManager>();

            if (PlayerPrefs.HasKey("HighScore"))
            {
                hiScore = PlayerPrefs.GetInt("HighScore");

            }
            else
            {
                hiScore = 0;
            }

            if (PlayerPrefs.HasKey("SecondPlace"))
            {
                secondScore = PlayerPrefs.GetInt("SecondPlace");
            }

            if (PlayerPrefs.HasKey("ThirdPlace"))
            {
                thirdScore = PlayerPrefs.GetInt("ThirdPlace");
            }

           if (returnFromBonusLevel)
            {
                Debug.Log("Bypassing title screen...");
                StartGame(autoDifficulty);
                returnFromBonusLevel = false;
                score = totalScore;

            }

            //  UpdateLeaderBoardScores();
            // UpdateLeaderBoardRankings();

           
    }

    public void StartGame (int difficulty) // distance between generated paths increases with difficulty selected
    {
        UpdateScore(totalScore);
        
        isGameActive = true;
        pathGenerator=FindObjectOfType<PathGenerator> ();
        
        
     pathGenerator.distanceBetweenMinPath1 *= difficulty;
     pathGenerator.distanceBetweenMaxPath1 *= difficulty;
      
     
     pathGenerator.distanceBetweenMinPath2 *= difficulty; 
     pathGenerator.distanceBetweenMaxPath2 *= difficulty;
        StartCoroutine(StartGeneratingPaths());
        titleScreen.gameObject.SetActive(false);
     
    }

   /* void StartReturnedGame()
    {
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        pathGenerator=FindObjectOfType<PathGenerator> ();
        StartCoroutine(StartGeneratingPaths());
        
    }*/
   IEnumerator StartGeneratingPaths() //calls path generator while the game is active
   {
   
       while (isGameActive) 
       {
           yield return new WaitForSeconds(0.2f);
           pathGenerator.GeneratePath1();
           pathGenerator.GeneratePath2();

           //yield return new WaitForSeconds(0.5f);
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
        totalScore = score;
        Debug.Log($"Score from Start Level: {score}, Total Score: {totalScore}");
        if (score > hiScore)
        {
            hiScore = score;
            PlayerPrefs.SetInt("HighScore",hiScore);
        }
        
        

        hiScoreText.text = "High Score: " + hiScore;
        UpdateLeaderBoardScores();

   }

   public void UpdateLeaderBoardScores()
   {
       firstScore = hiScore; 
       
       if (score>secondScore&&score<firstScore)
       {
           thirdScore = secondScore; 
           secondScore= score;
           PlayerPrefs.SetInt("SecondPlace", secondScore);
           
       }
       if (score > thirdScore && score < secondScore)
       {
           thirdScore = score;
           PlayerPrefs.SetInt("ThirdPlace", thirdScore);
           
       }
       
       firstPlaceScore.text = firstScore.ToString();
       secondPlaceScore.text = secondScore.ToString();

       thirdPlaceScore.text = thirdScore.ToString();
   }



   
    public void UpdateLives(int livesToTake)
    {
     
        lives += livesToTake;
        livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
       // UpdateLeaderBoard();
        replayButton.gameObject.SetActive(true);
        isGameActive = false;
       

    }

    public void RestartGame()
    {
        isGameActive = true;
        SceneManager.LoadScene("MyGame");
    }
    
}
