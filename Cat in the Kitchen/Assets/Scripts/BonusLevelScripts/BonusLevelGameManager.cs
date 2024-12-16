using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusLevelGameManager : MonoBehaviour
{

    public float bonusTimeCountdown;
    
    
    
   // private int score;
    public static int totalScore;
    public int addedScore;
    public int scoreFromBonusLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        bonusTimeCountdown -= Time.deltaTime;
     /*  if (bonusTimeCountdown <= 0)
        {
            SceneManager.LoadScene("MyGame");
        }*/
    }

    public void UpdateScore(int scoreToAdd)
    {


        scoreFromBonusLevel += scoreToAdd;
        totalScore += scoreToAdd; // Synchronize total score
        Debug.Log($"Score from Bonus Level: {scoreFromBonusLevel}, Total Score: {totalScore}");
       // score = totalScore;

    }
}
