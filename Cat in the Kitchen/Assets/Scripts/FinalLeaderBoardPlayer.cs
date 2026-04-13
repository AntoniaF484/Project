using UnityEngine;
using TMPro;

public class FinalLeaderBoardPlayer : MonoBehaviour
{

    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI scoreText;


    public void SetEntry(string playerName, int score)
    {
        playerNameText.text = playerName;
        scoreText.text = "Score " + score.ToString();
    
    }
}
