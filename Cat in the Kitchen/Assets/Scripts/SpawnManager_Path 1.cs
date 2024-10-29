using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Path1 : MonoBehaviour
{
    
public GameObject [] path1Prefabs;
public GameObject[] FoodPrefabs;
private PlayerController playerControllerScript;
private float spawnRangeY = 5f;
private float startPosY = 10f;
private float startDelay = 0f;
private float spawnInterval = 0.6f;
private float spawnPosX = 40f;

   // Start is called before the first frame update
 void Start()
    {
       InvokeRepeating ("SpawnRandomPath", startDelay, spawnInterval);
playerControllerScript = 
GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void SpawnRandomPath()
    {
        int path1PrefabsIndex = Random.Range(0, path1Prefabs.Length);
        if (playerControllerScript.gameOver==false){
Instantiate(path1Prefabs[path1PrefabsIndex], new Vector3(spawnPosX, Random.Range(startPosY-spawnRangeY, startPosY+ spawnRangeY), 0),
            path1Prefabs[path1PrefabsIndex].transform.rotation);}

       
    }

}
