using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObstaclePowerupGenerator : MonoBehaviour
{
    public ObjectPooler [] foodPool;
    private int foodSelector;
   
    public ObjectPooler [] obstaclePool;
    private int obstacleSelector;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    

    public void SpawnFood(Vector3 startPosition) //selects a random food from the different foods in the pool, sets as active in the scene
    {
        foodSelector = Random.Range(0,foodPool.Length);
        GameObject food1 = foodPool[foodSelector].GetPooledObject();
       food1.transform.position = startPosition; 
        food1.SetActive(true);

    }

    public void SpawnObstacle(Vector3 startPosition)//selects a random obstacle from the different obstacles in the pool, sets as active in the scene
    {

        obstacleSelector = Random.Range(0, obstaclePool.Length);
           
        GameObject obstacle1 = obstaclePool[obstacleSelector].GetPooledObject();
        obstacle1.transform.position = startPosition;
        obstacle1.SetActive(true);

    }
}

