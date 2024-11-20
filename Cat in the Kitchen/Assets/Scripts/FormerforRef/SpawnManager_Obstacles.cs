using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Obstacles : MonoBehaviour
{
 public GameObject[] ObstaclePrefabs; 
    public Transform spawnLocation; 
    // Start is called before the first frame update
    void Start()
    {
        int RandomNumber = Random.Range(0,100);
if (RandomNumber <20){

SpawnRandomObstacle();}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  void SpawnRandomObstacle()
    {
        int ObstaclePrefabsIndex = Random.Range(0, ObstaclePrefabs.Length);

        Instantiate(ObstaclePrefabs[ObstaclePrefabsIndex], spawnLocation.position, spawnLocation.rotation);
    }
}
