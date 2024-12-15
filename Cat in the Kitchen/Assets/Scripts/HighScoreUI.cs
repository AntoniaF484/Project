using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject highscoreUIElementPrefab;
    [SerializeField] Transform elementWrapper;
    
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI highscoreText;

    List<GameObject> uiElements = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log("Subscribing to highscore update event");
        HighScoreHandler.onHighscoreListChanged += UpdateUI;
    }
    private void OnDisable()
    {
        Debug.Log("Unsubscribing from highscore update event");
        HighScoreHandler.onHighscoreListChanged -= UpdateUI;
    }

    private void UpdateUI(List<HighScoreElement> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            HighScoreElement el = list[i];
            if (el.points > 0)
            {
                var inst = Instantiate(highscoreUIElementPrefab, Vector3.zero, Quaternion.identity);
                inst.transform.SetParent(elementWrapper, false);

                uiElements.Add(inst);
            }

            var texts = uiElements[i].GetComponentsInChildren<Text>();
            if (texts.Length >= 2)
            {
                texts[0].text = el.playerName;
                texts[1].text = el.points.ToString();
            }
           //TEST
            else
            {
                Debug.Log("HighScoreUI prefab is missing Text");
            }
        }
    }
}
