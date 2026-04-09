using System;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class IndividualPlayerStats : NetworkBehaviour
{
    public NetworkVariable<int> score = new NetworkVariable<int>(0);
    public NetworkVariable<int> lives = new NetworkVariable<int>(9);

    public int startingLives = 9;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI hiScoreText;
    
    private int hiScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
       
    }

    public override void OnNetworkSpawn()
    {
       
            lives.Value = startingLives;
            score.Value = 0;
        
        if (!IsOwner)
        {
            
            scoreText.gameObject.SetActive(false);
            hiScoreText.gameObject.SetActive(false);
            livesText.gameObject.SetActive(false);
        }
        else
        {
            lives.Value = startingLives;
            UpdateScoreUI();
            UpdateLivesUI();
        }
        score.OnValueChanged += (_, _) => UpdateScoreUI();
       lives.OnValueChanged += (_, _) => UpdateLivesUI();

    }

    public void UpdateScore(int scoreToAdd)
    {

        if (IsOwner)
        {
            AddScoreServerRpc(scoreToAdd);
        }

    }

    public void UpdateLives(int livesToAdd)
    {

        if (IsOwner)
        {
            TakeLivesServerRpc(livesToAdd);
        }

    }
    
    [ServerRpc(RequireOwnership = false)]
    void AddScoreServerRpc(int scoreToAdd)
    {
        
        score.Value += scoreToAdd;
       
     
        if (score.Value > hiScore)
        {
            hiScore = score.Value;
            PlayerPrefs.SetInt("HighScore",hiScore);
        }

    }

    [ServerRpc(RequireOwnership = false)]
    void TakeLivesServerRpc(int livesToAdd)
    {
        lives.Value += livesToAdd;
    }
    void UpdateScoreUI()
    {
        
        scoreText.text = "Score: " + score.Value;

      //  hiScoreText.text = "High Score: " + hiScore;
        
    }

    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives.Value;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
