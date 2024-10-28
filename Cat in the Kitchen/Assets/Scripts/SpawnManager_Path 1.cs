using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Path1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject [] path1Prefabs;
  
  private float spawnRangeY = 5f;
  private float startPosY = 10f;
  private float startDelay = 2;
  private float spawnInterval = 1.5f;

  

    void Start()
    {
       InvokeRepeating ("SpawnRandomPath", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnRandomPath();
          
        }
    }

    void SpawnRandomPath()
    {
        int path1PrefabsIndex = Random.Range(0, path1Prefabs.Length);
        Instantiate(path1Prefabs[path1PrefabsIndex], new Vector3(20, Random.Range(startPosY-spawnRangeY, startPosY+ spawnRangeY), 0),
            path1Prefabs[path1PrefabsIndex].transform.rotation);
    }
}
