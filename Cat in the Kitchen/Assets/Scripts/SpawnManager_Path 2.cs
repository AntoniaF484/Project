using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Path2 : MonoBehaviour
{
   
public GameObject [] path2Prefabs;

private PlayerController playerControllerScript;
private float spawnRangeX = 7f;
private float startPosX = 40f;
private float spawnRangeY = 7f;
private float startPosY = 30f;
private float startDelay = 0.2f;
private float spawnInterval= 0.6f;

  

void Start()
{
    InvokeRepeating ("SpawnRandomPath", startDelay, spawnInterval);
playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
}

// Update is called once per frame
void Update()
{
  
   
}

void SpawnRandomPath()
{
    int path2PrefabsIndex = Random.Range(0, path2Prefabs.Length);
   if (playerControllerScript.gameOver==false){
 Instantiate(path2Prefabs[path2PrefabsIndex], new Vector3(Random.Range(startPosX-spawnRangeX, startPosX+ spawnRangeX), Random.Range(startPosY-spawnRangeY, startPosY+ spawnRangeY), 0),
        path2Prefabs[path2PrefabsIndex].transform.rotation);}
}
}

