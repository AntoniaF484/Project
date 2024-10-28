using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Path1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject [] path1Prefabs;
    public GameObject[] FoodPrefabs;
    private float distY = 3.0f;
  private float spawnRangeY = 5f;
  private float startPosY = 10f;
  private float startDelay = 0f;
  private float spawnInterval = 0.6f;
private float spawnPosX = 40f;

  private List<GameObject> spawnedPath = new List<GameObject>();

    void Start()
    {
       InvokeRepeating ("SpawnRandomPath", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void SpawnRandomPath()
    {
        int path1PrefabsIndex = Random.Range(0, path1Prefabs.Length);
        GameObject newPath = Instantiate(path1Prefabs[path1PrefabsIndex], new Vector3(spawnPosX, Random.Range(startPosY-spawnRangeY, startPosY+ spawnRangeY), 0),
            path1Prefabs[path1PrefabsIndex].transform.rotation);

        spawnedPath.Add(newPath);
       
    }

    void SpawnFood(GameObject spawnedPath)
    {
        int FoodPrefabsIndex = Random.Range(0, FoodPrefabs.Length);
        Vector3 spawnPos = spawnedPath.transform.position;
        Instantiate(FoodPrefabs[FoodPrefabsIndex], new Vector3(spawnPos.x, (spawnPos.y + distY), 0),
            FoodPrefabs[FoodPrefabsIndex].transform.rotation);
    }
}
