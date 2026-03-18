using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    //private Vector3 startPos;

    public ObjectPooler[] theObjectPoolsBackground;
    private float[] backgroundWidths;
    private int backgroundSelector;

    private float distanceBetweenBackgrounds=140.9f; // distance accounts for correct overlap amount
    private Vector3 backgroundPosition;

    
    public Transform backgroundGenerationPoint;
    
// Backgrounds Y and Z positions are fixed
    private float backgroundY = 25.3f;
    private float backgroundZ = 2f;
    
    float offsetDistance = 200f;
    // Start is called before the first frame update
    void Start()
    {
       backgroundWidths = new float [theObjectPoolsBackground.Length];
       for (int i = 0; i < theObjectPoolsBackground.Length; i++)
       {
           backgroundWidths[i] = theObjectPoolsBackground[i].pooledObject.GetComponent<Renderer>().bounds.size.x;
       }
       
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();//Get all players in the scene, if there are no players then no action
        if (players.Length == 0) return;
        
        Transform leader = players[0].transform; //find player in the lead (furthest along x)
        foreach (var p in players)
        {
            if (p.transform.position.x > leader.position.x)
                leader = p.transform;
        }
        Vector3 newPos = backgroundGenerationPoint.position; //update generation point so always ahead of leading player
        newPos.x = Mathf.Max(newPos.x, leader.position.x + offsetDistance); // returns larger of newPos.x and the leader pos + offset to prevent generation point moving back
        backgroundGenerationPoint.position = newPos;
       
        if (backgroundPosition.x<backgroundGenerationPoint.position.x) // Selecting background and setting as active in below named position
        {
            backgroundSelector = Random.Range(0, theObjectPoolsBackground.Length);
            backgroundPosition = new Vector3(backgroundPosition.x + distanceBetweenBackgrounds, backgroundY,
                backgroundZ);
            
            GameObject newBackground = theObjectPoolsBackground [backgroundSelector].GetPooledObject();
            newBackground.transform.position = backgroundPosition;
            newBackground.transform.rotation = transform.rotation; 
            newBackground.SetActive(true); 
           
        }
      
    }
   /* Transform GetLeadingPlayer(PlayerController[] players)
    {
        if (players.Length == 0) return null;

        Transform leading = players[0].transform;
        foreach (var p in players)
        {
            if (p.transform.position.x > leading.position.x)
                leading = p.transform;
        }
        return leading;
    }*/
}
