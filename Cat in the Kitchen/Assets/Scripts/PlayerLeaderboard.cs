using UnityEngine;
using TMPro;

public class PlayerLeaderboard : MonoBehaviour
{

    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    public void SetEntry(string playerName, int score, int lives)
    {
        playerNameText.text = playerName;
        scoreText.text = "Score " + score.ToString();
        livesText.text = "Lives " + lives.ToString();
    
    }
}
