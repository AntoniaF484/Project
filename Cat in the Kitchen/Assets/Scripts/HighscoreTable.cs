using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;

    private Transform entryTemplate;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 30f;
        for (int i = 0; i < 5; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "TH";
                    break;
                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }
            entryTransform.Find("posText").GetComponent<Text>().text=rankString;
            int score = Random.Range(0, 10000);
            entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

            string name = "AAA";
            entryTransform.Find("nameText").GetComponent<Text>().text="";
            
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
