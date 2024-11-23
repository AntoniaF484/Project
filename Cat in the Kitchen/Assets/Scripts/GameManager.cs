using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

{
    private int score;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button replayButton;

    private PlayerController playerController;
    private PathGenerator pathGenerator;
    private Vector3 playerStartPoint;

    public Transform GenerationPoint;
    private Vector3 path1StartPoint;
    

   public bool isGameActive;
    public Transform PathGenerator;
    private Vector3 pathStartPoint;
    public GameObject titleScreen;
 
    void Start()
    {
      //StartGame();

    }

    public void StartGame()
    {
        UpdateScore(0);
        isGameActive = true;
        pathGenerator=FindObjectOfType<PathGenerator> ();
        
        StartCoroutine(StartGeneratingPaths());
        titleScreen.gameObject.SetActive(false);
    }

   /* void Update()
    {
        StartGeneratingPaths();
    }
    */
   IEnumerator StartGeneratingPaths()
   {
   
       while (isGameActive) 
       {
           pathGenerator.GeneratePath1();
           pathGenerator.GeneratePath2();

           yield return new WaitForSeconds(0.5f);
       }
   }

   public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true);
        isGameActive = false;

    }

    public void RestartGame()
    {
        isGameActive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
