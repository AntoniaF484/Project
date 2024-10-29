using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Food : MonoBehaviour
{
   

 public GameObject[] FoodPrefabs; 
    public Transform spawnLocation; 
    // Start is called before the first frame update
    void Start()
    {
      int RandomNumber = Random.Range(0,100);
if (RandomNumber <90){
SpawnRandomFood();}
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
