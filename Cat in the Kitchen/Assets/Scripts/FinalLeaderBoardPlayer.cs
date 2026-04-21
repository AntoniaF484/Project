using UnityEngine;
using TMPro;

public class FinalLeaderBoardPlayer : MonoBehaviour
{

    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI scoreText;


    public void SetEntry(string playerName, int score) // set players entry in the final leaderboard, based on players stats
    {
        playerNameText.text = playerName;
        scoreText.text = "Score " + score.ToString();
    
    }
}
