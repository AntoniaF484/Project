using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelSpawnManager : MonoBehaviour


{

    public float spawnRange;
    public GameObject[] foodPrefabs;
    public int maxFood;
    private int foodCounter;
    

    // Start is called before the first frame update
    void Start()
    {
        foodCounter = 0;
        InvokeRepeating ("SpawnRandomFood", 1, 0.8f);

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void SpawnRandomFood()
    {
        if (foodCounter < maxFood)
        {
            int foodIndex = Random.Range(0, foodPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), 1.5f,
                Random.Range(-spawnRange, spawnRange));
            Instantiate(foodPrefabs[foodIndex], spawnPos, foodPrefabs[foodIndex].transform.rotation);
            foodCounter++;
        }
    }

   /* private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;

    }*/

}

