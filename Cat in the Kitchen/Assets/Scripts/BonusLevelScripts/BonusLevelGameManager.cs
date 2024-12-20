using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BonusLevelGameManager : MonoBehaviour
{

    public float bonusTimeCountdown; // time allowed in bonus scene
    public TextMeshProUGUI bonusScoreText;
    public int addedScore;
    public int scoreFromBonusLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        bonusScoreText.text = "Score: " + GameManager.totalScore; // when bonus level starts, score is made consistent with the score from first level
       
    }

    // Update is called once per frame
    void Update()
    {
        bonusTimeCountdown -= Time.deltaTime;
      if (bonusTimeCountdown <= 0)
        {
            SceneManager.LoadScene("EndGame"); // When time in the Bonus scene runs out, load the EndGame scene
            
        }
    }

    public void UpdateScore(int scoreToAdd)
    {


        scoreFromBonusLevel  += scoreToAdd;
        GameManager.totalScore += scoreFromBonusLevel; // Add the score from the Bonus level to the overall score
        bonusScoreText.text = "Score: " + GameManager.totalScore;
        

    }
}
