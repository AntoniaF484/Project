using UnityEngine;
using TMPro;

public class PlayerLeaderboard : MonoBehaviour
{

    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI scoreText;

    public void SetEntry(ulong clientId, int score)
    {
        playerNameText.text = $"Player {clientId}";
        scoreText.text = score.ToString();
    
    }
}
