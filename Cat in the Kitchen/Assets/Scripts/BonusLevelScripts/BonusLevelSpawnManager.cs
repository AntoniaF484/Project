using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelSpawnManager : MonoBehaviour


{ 
    public float spawnRange; // size of square in which food is instantiated in bonus
    public GameObject[] foodPrefabs;
    public int maxFood;// maximum amount of food to be generated in the bonus level
    private int foodCounter;//current amount of food in bonus scene
    

    // Start is called before the first frame update
    void Start()
    {
        foodCounter = 0; // sets current amount of food in bonus scene as zero
        InvokeRepeating ("SpawnRandomFood", 1, 0.5f);

    }

  
    
    void SpawnRandomFood()
    {
        if (foodCounter < maxFood) // if there is less food than the maximum allowed, generate food
        {
            int foodIndex = Random.Range(0, foodPrefabs.Length); //Picks a food from the prefabs
            
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), 1.5f,
                Random.Range(-spawnRange, spawnRange)); // Food spawn location
            
            Instantiate(foodPrefabs[foodIndex], spawnPos, foodPrefabs[foodIndex].transform.rotation);
            
            foodCounter++; // adds 1 to the food counter when a new food is created
        }
    }
    
}

