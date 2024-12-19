using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    //private Vector3 startPos;

    public ObjectPooler[] theObjectPoolsBackground;
    private float[] backgroundWidths;
    private int backgroundSelector;

    private float distanceBetweenBackgrounds;
    private Vector3 backgroundPosition;

    public Transform backgroundGenerationPoint;

    private float backgroundY = 25.3f;

    private float backgroundZ = 2f;
    // Start is called before the first frame update
    void Start()
    {
       // startPos=transform.position;
       backgroundWidths = new float [theObjectPoolsBackground.Length];
       for (int i = 0; i < theObjectPoolsBackground.Length; i++)
       {
           backgroundWidths[i] = theObjectPoolsBackground[i].pooledObject.GetComponent<Renderer>().bounds.size.x;
       }

       distanceBetweenBackgrounds = 140.9f;
    }

    // Update is called once per frame
    void Update()
    {

        if (backgroundPosition.x<backgroundGenerationPoint.position.x)
        {
            backgroundSelector = Random.Range(0, theObjectPoolsBackground.Length);
            backgroundPosition = new Vector3(backgroundPosition.x + distanceBetweenBackgrounds, backgroundY,
                backgroundZ);
            
            GameObject newBackground = theObjectPoolsBackground [backgroundSelector].GetPooledObject();
            newBackground.transform.position = backgroundPosition;
            newBackground.transform.rotation = transform.rotation; 
            newBackground.SetActive(true); //setting platform as active in above named position
        }
      //  if (transform.position.x < startPos.x - 50)
       // {
       //     transform.position = startPos;
       // }
    }
}
