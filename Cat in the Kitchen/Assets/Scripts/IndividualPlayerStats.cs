using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;

public class IndividualPlayerStats : NetworkBehaviour
{
    public NetworkVariable<int> score = new NetworkVariable<int>(0);
    public NetworkVariable<int> lives = new NetworkVariable<int>(9);
    
    public NetworkVariable<FixedString32Bytes> playerName =
        new NetworkVariable<FixedString32Bytes>();

    public NetworkVariable<bool> isReady =
        new NetworkVariable<bool>(false);

    public int startingLives = 9;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI hiScoreText;
    
    private int hiScore;
    
    public override void OnNetworkSpawn()
    {
       
        score.OnValueChanged += (oldValue,newValue) => UpdateScoreUI(); // subscribe to the onvaluechanged event for the network variable score and update the score UI when it changes
        lives.OnValueChanged += (oldValue, newValue) => UpdateLivesUI(); //subscribe to the onvaluechanged event for network variable lives and update the score UI when it changes
        
        if (!IsOwner) // only show for the owner
        {
            
            scoreText.gameObject.SetActive(false);
            hiScoreText.gameObject.SetActive(false);
            livesText.gameObject.SetActive(false);
        }
        else // shows correct values at spawning
        {
            
            UpdateScoreUI();
            UpdateLivesUI();
        }
       

    }

    public void UpdateScore(int scoreToAdd)
    {

        if (IsOwner) // the owner (relevant player) sends a request to the server to update score
        {
            AddScoreServerRpc(scoreToAdd);
        }

    }

    public void UpdateLives(int livesToAdd) // owner sends request to server to update lives count
    {
        if (IsOwner)
        {
            TakeLivesServerRpc(livesToAdd);
        }
    }
    
    [ServerRpc(RequireOwnership = false)]
    void AddScoreServerRpc(int scoreToAdd) // server changes the score by adding the scoretoadd to the existing score
    {
        
        score.Value += scoreToAdd;

    }

    [ServerRpc(RequireOwnership = false)] //server updates amount of lives
    void TakeLivesServerRpc(int livesToAdd)
    {
        lives.Value += livesToAdd;
    }
    void UpdateScoreUI() // updates players own score UI
    {
        scoreText.text = "Score: " + score.Value;
    }

    void UpdateLivesUI() // update players lives count
    {
        livesText.text = "Lives: " + lives.Value;
    }
    
    [ServerRpc(RequireOwnership = false)] // server sets the players name based on what they input at the start
    public void SetPlayerNameServerRpc(string newName)
    {
        playerName.Value = newName;
    }
    [ServerRpc(RequireOwnership = false)]// server sets ready status for player
    public void SetReadyServerRpc(bool ready)
    {
        isReady.Value = ready;
    }

}
