
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Netcode;


public class GameManager : NetworkBehaviour

{
    
    //UI
        //UI For players own view
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    
        //UI For world view
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI hiScoreText;
    public Button replayButton;
    public GameObject titleScreen;

   
        //Start game UI
        public TMP_Dropdown expectedPlayersDropdown;
        // Scoreboard
        public TextMeshProUGUI firstPlaceScore;
        private int firstScore;
        public TextMeshProUGUI secondPlaceScore;
        private int secondScore;
        public TextMeshProUGUI thirdPlaceScore;
        private int thirdScore;
        
//Other scripts
    private PlayerController playerController;
    private PathGenerator pathGenerator;
    private PowerUpManager powerUpManager;

   

// Player movement
    private int score;
    public static int totalScore;
    public int addedScore;
//player stats
    public int lives;
    public static int totalLives;
    public int hiScore;
    
    //Path generation/game setup
   public bool isGameActive;
    public Transform PathGenerator;
    public bool returnFromBonusLevel;
    public int autoDifficulty = 3;

         //Distance Between Path Variables
    private float distanceBetweenMinPath1;
    private float distanceBetweenMinPath2;

    private float distanceBetweenMaxPath1;
    private float distanceBetweenMaxPath2;

    private float originalDistanceMax; 
    private float originalDistanceMin;
   


   // Player joining
    [SerializeField] private GameObject [] playerPrefab; //possible cat prefabs to spawn when the player joins
    private List<GameObject> availableCats; // list of available prefabs (not yet spawned)
    
    public NetworkVariable<bool> allReady = new(readPerm: NetworkVariableReadPermission.Everyone,
        writePerm: NetworkVariableWritePermission.Server); //bool to mark if all the players in the game are ready for it to start

    public int defaultExpectedPlayers=4;// pre-setting default players to 4 (max possible) allows other players to join before the host has set expected players. Not concerned with too many joining as expected players is to make sure the game doesnt start before all players have joined and are ready
    private int readyPlayers; // total plays who have pressed ready button
    public GameObject joinScreen; //join screen which will be deactivated once the game starts

    private int spawnedCats = 0; //number of players who have joined the game
    public NetworkVariable<int> expectedPlayers = new(4, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);//number of players expected

    void Start()
    
    {
        if (NetworkManager.Singleton.IsServer)
        {
            expectedPlayers.Value = defaultExpectedPlayers; // at the start of the game, expected players is set by the server as the default expected players
        }
        
        availableCats =new List<GameObject>(playerPrefab); // list of available cat prefabs
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += SpawnCatForClient; // cat is spawned when a player connects
        }
        
        pathGenerator = FindObjectOfType<PathGenerator>();
        powerUpManager = FindObjectOfType<PowerUpManager>();
     
    }
    public void OnPlayerCountChanged(int index) // linked to player count dropdown
    {
        if (!NetworkManager.Singleton.IsServer) return; // expected players is only set by the server

        expectedPlayers.Value = index; // sets expected players. Index corresponds to the position of the number of players in the dropdown list
    }

    public void StartGame (int difficulty) // distance between generated paths increases with difficulty set 
    {
        isGameActive = true;
        pathGenerator=FindObjectOfType<PathGenerator> ();
  
     pathGenerator.distanceBetweenMinPath1 *= difficulty;
     pathGenerator.distanceBetweenMaxPath1 *= difficulty;
  
     pathGenerator.distanceBetweenMinPath2 *= difficulty; 
     pathGenerator.distanceBetweenMaxPath2 *= difficulty; 
     
     titleScreen.gameObject.SetActive(false);
     
    }
    

    public void GameOver() // when the game is over, this brings up the game over message and stops the players movement
    {
        gameOverText.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true);
        isGameActive = false;
        PlayerController player = FindObjectOfType<PlayerController>();
        player.enableMovement = false;
        Rigidbody playerRb = FindObjectOfType<PlayerController>().GetComponent<Rigidbody>();
        playerRb.linearVelocity = Vector3.zero;
       


    }
    
    void Update()
    {
        if (allReady.Value){ joinScreen.gameObject.SetActive(false);} // if all players are ready, deactivate the join game screen
    }
    private void SpawnCatForClient(ulong clientId)
    {
        if (!NetworkManager.Singleton.IsServer) return; //only server can spawn cats
        if (spawnedCats >= expectedPlayers.Value) // do not spawn more cats if expected players is exceeded
        {
            return;
        }

        spawnedCats++;// spawns a new cat
        int prefabIndex = Random.Range(0, availableCats.Count); //pick number at random based on number of cat prefabs
        GameObject prefabToSpawn = availableCats [prefabIndex]; //corresponding cat to be spawned
        availableCats.RemoveAt(prefabIndex); // remove spawned cat from list
        GameObject playerInstance = Instantiate(prefabToSpawn); //in server/host the prefab is instantiated
        NetworkObject netObj = playerInstance.GetComponent<NetworkObject>(); //get the nework object on the spawned cat (to synch with Client)
        netObj.SpawnAsPlayerObject(clientId, true); //Client owns this playerobject
        
        IndividualPlayerStats playerStats = playerInstance.GetComponent<IndividualPlayerStats>();
        if (playerStats != null) // sets the players starting lives and score
        {
            playerStats.lives.Value = playerStats.startingLives;
            playerStats.score.Value = 0;
        }
        
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void PlayerReadyServerRpc() // when a player clicks that they are ready, add one to ready players. When readyplayers matches expected players, everyone is ready (allready=true)
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
            expectedPlayersDropdown.gameObject.SetActive(NetworkManager.Singleton.IsHost);// only the host can see the expected player number dropdown
        }
        
    }
}
