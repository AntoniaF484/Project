using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public ObjectPooler [] foodPool;
   private int foodSelector;
   
   public ObjectPooler [] obstaclePool;
   private int obstacleSelector;
   
   
  //  public float distanceBetweenFood;
  //  public Transform foodLocation;

    // Start is called before the first frame update
    void Start()
    {
       // int RandomNumber = Random.Range(0, 100);
        //if (RandomNumber < 90)
        {
            // SpawnFood();
        }
    }

    // Update is called once per frame
        void Update()
        {

        }

       public void SpawnFood(Vector3 startPosition)
        {
            foodSelector = Random.Range(0,foodPool.Length);
           // GameObject food1 = foodPool[foodSelector].GetPooledObject();
           GameObject food1 = foodPool[foodSelector].GetPooledObject();
            food1.transform.position = startPosition; 
            food1.SetActive(true);

        }

        public void SpawnObstacles(Vector3 startPosition)
        {

            obstacleSelector = Random.Range(0, obstaclePool.Length);
           
            GameObject obstacle1 = obstaclePool[obstacleSelector].GetPooledObject();
            obstacle1.transform.position = startPosition;
            obstacle1.SetActive(true);

        }
}

