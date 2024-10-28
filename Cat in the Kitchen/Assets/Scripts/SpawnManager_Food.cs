using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Food : MonoBehaviour
{
    private float startDelay = 0.2f;
    private float spawnIntervalRange = 0.4f;

    private float distY = 3.0f;
    public GameObject[] FoodPrefabs; 
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomFood", startDelay, (Random.Range(-spawnIntervalRange, spawnIntervalRange)));
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnRandomFood()
    {
        int FoodPrefabsIndex = Random.Range(0, FoodPrefabs.Length);
        Instantiate(FoodPrefabs[FoodPrefabsIndex],
            new Vector3(3, distY, 0),
            FoodPrefabs[FoodPrefabsIndex].transform.rotation);
    }
}
