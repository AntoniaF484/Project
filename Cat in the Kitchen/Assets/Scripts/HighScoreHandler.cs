using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighScoreHandler : MonoBehaviour
{
    private List<HighScoreElement> highscoreList = new List<HighScoreElement>();

    [SerializeField] private int maxCount = 3;

    [SerializeField] private string filename;

    public delegate void OnHighscoreListChanged(List<HighScoreElement> list);

    public static event OnHighscoreListChanged onHighscoreListChanged;

    public bool hasScoreBeenAdded = false;

    // Start is called before the first frame update
    void Start()
    {

        if (hasScoreBeenAdded)
        {
            LoadHighscores();
            hasScoreBeenAdded = true;
        }
        
    }

    private void LoadHighscores()
    {
        highscoreList = FileHandler.ReadListFromJSON<HighScoreElement>(filename);
        Debug.Log("Loaded Highscores:");
        
        foreach (var score in highscoreList)
        {
            Debug.Log($"Player: {score.playerName}, Score: {score.points}");
        }

        while (highscoreList.Count > maxCount)
        {
            highscoreList.RemoveAt(maxCount);
        }

        if (onHighscoreListChanged != null)
        {
            onHighscoreListChanged.Invoke(highscoreList);
        }

    }

    private void SaveHighScore()
    {
      
        if (!hasScoreBeenAdded)
            FileHandler.SaveToJSON<HighScoreElement>(highscoreList, filename);
    }

    public void AddHighscoreIfPossible(HighScoreElement element)
    {
      
        for (int i = 0; i < maxCount; i++)
        {

            if (i >= highscoreList.Count || element.points > highscoreList[i].points)
            {
                highscoreList.Insert(i, element); //adding new high score
                hasScoreBeenAdded = true;

            }


        }

    }
}
  
