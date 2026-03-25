using System;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(Camera))]
public class SplitScreen : NetworkBehaviour
{
    private NetworkVariable<int> playerCount = new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone);
    private Camera cam;
    

    private int index; //number is assigned to each player as they joing the game

   // [SerializeField] private GameObject [] playerPrefab;
   // private int totalPlayers;

    private void Awake()
    {
        cam=GetComponent<Camera>();
        
    }

    public override void OnNetworkSpawn()
    {
        index = GetPlayerIndex(); //determine players index
        cam.depth = index; // set camera order
        playerCount.OnValueChanged += OnPlayerCountChanged;

        if (IsServer && NetworkManager.Singleton != null)
        {
            UpdatePlayerCount(); //sets initial playercount (server is player)

            NetworkManager.Singleton.OnClientConnectedCallback += OnClientChanged;//update player count when someone joins
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientChanged; //update playercount when someone leaves
        }
        SetUpCamera(playerCount.Value); //set up camera in line with number of players
    }
    private void OnClientChanged(ulong _) //ulong =Client ID of player who joined or left in Netcode
    {
        UpdatePlayerCount(); //calls update playercount when player joins/eaves
    }

    private void UpdatePlayerCount()
    {
        playerCount.Value = NetworkManager.Singleton.ConnectedClients.Count; //sets network variable for player count
    }

    private int GetPlayerIndex() //finds players index based on their Client ID
    {
        var clients = NetworkManager.Singleton.ConnectedClientsList;

        for (int i = 0; i < clients.Count; i++)
        {
            if (clients[i].ClientId == OwnerClientId)
                return i;
        }

        return 0;
    }

    private void OnPlayerCountChanged(int oldValue, int newValue) //if player count is changed, change camera setup
    {
        SetUpCamera(newValue);
    }


    private void SetUpCamera(int totalPlayers)
    {
        if (totalPlayers == 1)
        {
            cam.rect = new Rect(0f, 0f, 1f, 1f); //full screen if 1 player
        }
        
        else if (totalPlayers == 2)
        {
            cam.rect = new Rect(0f, index ==0?0.5f: 0f, 1f, 0.5f); //horizontal split if 2 players. (index ==0?0.5f - if player index is 0, y=0.5 and player zero is in top half)
        }
        else if (totalPlayers == 3)
        {
            cam.rect = new Rect(index ==0?0 : (index ==1?0.5f:0),
            index <2 ? 0.5f:0,
                index<2?0.5f:1,
                    0.5f);
                    
         
        }
        else
        {
            cam.rect = new Rect(index % 2 * 0.5f, (index < 2) ? 0.5f : 0f, 0.5f, 0.5f); // 2x2 grid - (X pos - index%2*0.5f remainder when/2, e.g. player 4 =index 3. 3%2 =1.5, x=0.5) y position - when index is smaller than 2, y=0.5 (top row)
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        cam = GetComponent<Camera>();
        cam.depth = index;
       
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
