using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public GameObject DestructionPoint;
    public float offsetX=-200f;
    // Start is called before the first frame update
    void Start()
    {
        DestructionPoint = GameObject.Find("DestructionPoint");
    }

    // Update is called once per frame
    void Update() //Deactivates objects once they pass the destruction point (set as a child of the main camera)
    {
        Transform slowestPlayer = GetSlowestPlayer(); //get transform of slowest player
        if (slowestPlayer == null) return;
        Vector3 targetPos = DestructionPoint.transform.position;
        targetPos.x = slowestPlayer.position.x + offsetX; //destruction point should be X back from the slowest player (X is -ve)
        DestructionPoint.transform.position = targetPos;
        if (transform.position.x<DestructionPoint.transform.position.x)//objects will deactivate once past the destruction point
        {
            gameObject.SetActive(false);
        }
        
    }
    Transform GetSlowestPlayer()
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();//Get all players in the scene, if there are no players then no action
        if (players.Length == 0) return null;

        Transform slowest = players[0].transform;//find player in last place (least far along x)
        foreach (var p in players)
        {
            if (p.transform.position.x < slowest.position.x)
                slowest = p.transform;
        }
        return slowest;
    }
    
    
}
