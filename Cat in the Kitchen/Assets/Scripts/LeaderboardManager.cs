using System.Collections.Generic;
using Unity.Netcode;

public class LeaderboardManager : NetworkBehaviour // handles the leaderboard
{

    public List<PlayerLeaderboard> leaderboardEntries; // UI elements to display the leaderboard
    public IndividualPlayerStats playerStats;
    private GameManager gameManager;
    public List<FinalLeaderBoardPlayer> finalLeaderboardEntries;
    void Start()
    {
        playerStats = GetComponent<IndividualPlayerStats>(); // need player stats script as this is what is used for the clients name input
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        UpdateLeaderboard();
    }

    void UpdateLeaderboard()

    { 
        List<IndividualPlayerStats> allPlayers = new List<IndividualPlayerStats>();// creates a list to store the players stats
        if (NetworkManager.Singleton == null) // stop if no network manager yet
            return;

        IReadOnlyList<NetworkClient> clients = NetworkManager.Singleton.ConnectedClientsList; // get list of all the connected clients

        if (clients == null) // stop if the list is emty
            return;

        foreach (NetworkClient client in clients) // for each connected client, get the player object and attached stats component. Add the stats to the stats list
        {
            NetworkObject playerObj = client.PlayerObject;
            if (playerObj == null) continue;

            IndividualPlayerStats stats = playerObj.GetComponent<IndividualPlayerStats>();
            if (stats == null) continue;

            allPlayers.Add(stats);
        }

        allPlayers.Sort((a, b) => b.score.Value.CompareTo(a.score.Value));// Sort players by high to low score

        if (!gameManager.gameOver.Value)
        {

            for (int i = 0; i < leaderboardEntries.Count; i++)
            {
                if (i < allPlayers.Count) //update UI with playername and score
                {
                    leaderboardEntries[i].SetEntry(
                        allPlayers[i].playerName.Value.ToString(),
                        allPlayers[i].score.Value, allPlayers[i].lives.Value
                    );
                    leaderboardEntries[i].gameObject.SetActive(true); // make the used leaderboard entry visible
                }
                else
                {
                    leaderboardEntries[i].gameObject.SetActive(false); // hide unused entries
                }
            }
        }
        else
        {
            for (int i = 0; i < finalLeaderboardEntries.Count; i++)
            {
                if (i < allPlayers.Count) //update UI with playername and score
                {
                    finalLeaderboardEntries[i].SetEntry(
                        allPlayers[i].playerName.Value.ToString(),
                        allPlayers[i].score.Value);
                    finalLeaderboardEntries[i].gameObject.SetActive(true); // make the used leaderboard entry visible
                }
                else
                {
                    finalLeaderboardEntries[i].gameObject.SetActive(false); // hide unused entries
                }
            }
        }
    }
   
}

