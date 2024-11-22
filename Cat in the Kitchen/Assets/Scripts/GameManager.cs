using System.Collections;
using System.Collections.Generic;
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

    public PlayerController playerController;
    private Vector3 playerStartPoint;

    public Transform GenerationPoint;
    private Vector3 path1StartPoint;
    

   public bool isGameActive;
    public Transform PathGenerator;
    private Vector3 pathStartPoint;

  // public PlayerController Player;
 //private Vector3 playerStartPoint;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);
        isGameActive = true;

     //   path1StartPoint = PathGenerator.position;
     //  playerStartPoint = playerController.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        
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
       // StartCoroutine("RestartGameCo");
        isGameActive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

   /* public IEnumerator RestartGameCo()
    {

        yield return new WaitForSeconds(1f);
         isGameActive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     //  playerController.gameObject.SetActive(false);
     //  playerController.transform.position = playerStartPoint;
     //  PathGenerator.position = path1StartPoint;
     //  playerController.gameObject.SetActive(true);
    }*/
}
