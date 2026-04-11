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
    
    
    
    public NetworkVariable<bool> allReady = new(readPerm: NetworkVariableReadPermission.Everyone,
        writePerm: NetworkVariableWritePermission.Server);

    public int defaultExpectedPlayers=4;
    private int readyPlayers;
    public TMP_Dropdown expectedPlayersDropdown;

    public GameObject joinScreen;

    private int spawnedCats = 0;
    public NetworkVariable<int> expectedPlayers = new(
        4,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    void Start()
    
    {
        if (NetworkManager.Singleton.IsServer)
        {
            expectedPlayers.Value = defaultExpectedPlayers;
        }
        availableCats =new List<GameObject>(playerPrefab);
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += SpawnCatForClient;
        }
        pathGenerator = FindObjectOfType<PathGenerator>();
      
        powerUpManager = FindObjectOfType<PowerUpManager>();
     
    }
    public void OnPlayerCountChanged(int index)
    {
        if (!NetworkManager.Singleton.IsServer) return;

        expectedPlayers.Value = index;
        
        
    }

    public void StartGame (int difficulty) // distance between generated paths increases with difficulty selected
    {
        
        
        
        
        isGameActive = true;
        pathGenerator=FindObjectOfType<PathGenerator> ();
        
       
        
     pathGenerator.distanceBetweenMinPath1 *= difficulty;
     pathGenerator.distanceBetweenMaxPath1 *= difficulty;
      
     
     pathGenerator.distanceBetweenMinPath2 *= difficulty; 
     pathGenerator.distanceBetweenMaxPath2 *= difficulty;
     
     
     
        titleScreen.gameObject.SetActive(false);
     
    }
    

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

    void Update()
    {
        if (allReady.Value){ joinScreen.gameObject.SetActive(false);}
    }
    private void SpawnCatForClient(ulong clientId)
    {
        if (!NetworkManager.Singleton.IsServer) return; //only server can spawn cats
        if (spawnedCats >= expectedPlayers.Value)
        {
            return;
        }

        spawnedCats++;
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
        
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void PlayerReadyServerRpc()
    {
        readyPlayers++ ;

        if (readyPlayers >= expectedPlayers.Value)
        {
            allReady.Value = true;
           
        }
    }
    public override void OnNetworkSpawn()
    {
        if (NetworkManager.Singleton != null)
        {
            expectedPlayersDropdown.gameObject.SetActive(NetworkManager.Singleton.IsHost);
        }
        
    }
}
