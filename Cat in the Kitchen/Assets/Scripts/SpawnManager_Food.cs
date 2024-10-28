using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Food : MonoBehaviour
{
    private float startDelay = 0f;
    //private float spawnIntervalRange = 0.4f;
private float spawnInterval = 0.6f;

   // private float distY = 0.7f;
    public GameObject[] FoodPrefabs; 
    public Transform spawnLocation; 
    // Start is called before the first frame update
    void Start()
    {
       // InvokeRepeating("SpawnRandomFood", startDelay, spawnInterval);
SpawnRandomFood();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnRandomFood()
    {
        int FoodPrefabsIndex = Random.Range(0, FoodPrefabs.Length);

        Instantiate(FoodPrefabs[FoodPrefabsIndex], spawnLocation.position, spawnLocation.rotation);
    }
}
