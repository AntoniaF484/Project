using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Netcode;

public class GameManager : NetworkBehaviour

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
    public static int totalLives;
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
    
    public TextMeshProUGUI firstPlaceScore;
    private int firstScore;
    public TextMeshProUGUI secondPlaceScore;
    private int secondScore;
    public TextMeshProUGUI thirdPlaceScore;
    private int thirdScore;


    public bool returnFromBonusLevel;
   
    public int autoDifficulty = 3;
    
    
  
    private float originalDistanceMax; 
    private float originalDistanceMin;
    
    
    [SerializeField] private GameObject [] playerPrefab;
    private List<GameObject> availableCats;

    void Start()
    
    {
        availableCats =new List<GameObject>(playerPrefab);
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += SpawnCatForClient;
        }
        pathGenerator = FindObjectOfType<PathGenerator>();
      
        



      /* if (!returnFromBonusLevel)
        {
            lives = 9;
            totalLives = lives;
            livesText.text = "Lives: " + lives;
        }

        else
        {
           lives=totalLives;
           livesText.text = "Lives: " + lives;
            
        } */
        powerUpManager = FindObjectOfType<PowerUpManager>();

      /*      if (PlayerPrefs.HasKey("HighScore"))
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
               
                StartGame(autoDifficulty);
                
                   
                score = totalScore;
                lives = totalLives;

            }
*/
          
           
    }

    public void StartGame (int difficulty) // distance between generated paths increases with difficulty selected
    {
        
        
        //UpdateScore(totalScore);
        
        isGameActive = true;
        pathGenerator=FindObjectOfType<PathGenerator> ();
        
       
        
     pathGenerator.distanceBetweenMinPath1 *= difficulty;
     pathGenerator.distanceBetweenMaxPath1 *= difficulty;
      
     
     pathGenerator.distanceBetweenMinPath2 *= difficulty; 
     pathGenerator.distanceBetweenMaxPath2 *= difficulty;
     
     
     
        titleScreen.gameObject.SetActive(false);
     
    }
    
  /* public void UpdateScore(int scoreToAdd)
   {

      
       score += scoreToAdd;
        scoreText.text = "Score: " + score;
        totalScore = score;
     
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
        Debug.Log($"Lives from Start Level: {lives}, Total Lives: {totalLives}");
        lives += livesToTake;
        livesText.text = "Lives: " + lives;
        totalLives = lives;
        
    }*/

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true);
        isGameActive = false;
        
        
        PlayerController player = FindObjectOfType<PlayerController>();
        player.enableMovement = false;
        Rigidbody playerRb = FindObjectOfType<PlayerController>().GetComponent<Rigidbody>();
        playerRb.linearVelocity = Vector3.zero;
       


    }

    public void RestartGame()
    {
        isGameActive = true;
        SceneManager.LoadScene("MyGame");
        totalScore = 0;
    }
    
    private void SpawnCatForClient(ulong clientId)
    {
        if (!NetworkManager.Singleton.IsServer) return; //only server can spawn cats
      
        int prefabIndex = Random.Range(0, availableCats.Count); //pick number at random based on number of cat prefabs
        GameObject prefabToSpawn = availableCats [prefabIndex]; //corresponding cat to be spawned
        availableCats.RemoveAt(prefabIndex);
        GameObject playerInstance = Instantiate(prefabToSpawn); //in server/host the prefab is instantiated
        NetworkObject netObj = playerInstance.GetComponent<NetworkObject>(); //get the nework object on the spawned cat (to synch with Client)
        netObj.SpawnAsPlayerObject(clientId, true); //Client owns this playerobject
        
        IndividualPlayerStats playerStats = playerInstance.GetComponent<IndividualPlayerStats>();
        if (playerStats != null)
        {
            playerStats.lives.Value = playerStats.startingLives;
            playerStats.score.Value = 0;
        }
        else
        {
            Debug.LogError("IndividualPlayerStats not found on player prefab!");
        }
    }
    
}
