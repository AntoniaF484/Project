using UnityEngine;
using TMPro;

public class PlayerLeaderboard : MonoBehaviour
{
// this script is applied to each potential leaderboard entry, and corresponding player name, lives amount, and current score. refer to leaderboard manager for how these are updated
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
