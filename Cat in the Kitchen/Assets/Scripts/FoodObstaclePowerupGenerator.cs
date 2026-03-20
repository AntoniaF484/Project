using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FoodObstaclePowerupGenerator : MonoBehaviour
{
    public ObjectPooler [] foodPool;
    private int foodSelector;
   
    public ObjectPooler [] obstaclePool;
    private int obstacleSelector;

    public ObjectPooler[] powerUpPool;
    private int powerUpSelector;

    public ObjectPooler[] bonusPool;
    private int bonusSelector;
    

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
        NetworkObject netObj = food1.GetComponent<NetworkObject>(); //get networkobject attached to prefab
        if (netObj != null && !netObj.IsSpawned && NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer) //spawn object if it has a networkobject,hasnt been spawned already, and in the scene there is a networkmanager/in the server
        {
            netObj.Spawn(); //spawns on network
        }

    }

    public void SpawnObstacle(Vector3 startPosition)//selects a random obstacle from the different obstacles in the pool, sets as active in the scene
    {

        obstacleSelector = Random.Range(0, obstaclePool.Length);
           
        GameObject obstacle1 = obstaclePool[obstacleSelector].GetPooledObject();
        obstacle1.transform.position = startPosition;
        obstacle1.SetActive(true);
        NetworkObject netObj = obstacle1.GetComponent<NetworkObject>(); //get networkobject attached to prefab
        if (netObj != null && !netObj.IsSpawned && NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer) //spawn object if it has a networkobject,hasnt been spawned already, and in the scene there is a networkmanager/instance is the server
        {
            netObj.Spawn();//spawns on network
        }
    }

    public void SpawnPowerUp(Vector3 startPosition)
    {
        powerUpSelector = Random.Range(0, powerUpPool.Length);
           
        GameObject powerUp1 = powerUpPool[powerUpSelector].GetPooledObject();
        powerUp1.transform.position = startPosition;
        powerUp1.SetActive(true);
        NetworkObject netObj = powerUp1.GetComponent<NetworkObject>();//get networkobject attached to prefab
        if (netObj != null && !netObj.IsSpawned && NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer)//spawn object if it has a networkobject,hasnt been spawned already, and in the scene there is a networkmanager/instance is the server
        {
            netObj.Spawn();//spawns on network
        }
    }
    
    public void SpawnBonus(Vector3 startPosition)
    {
        bonusSelector = Random.Range(0, bonusPool.Length);   
        GameObject Bonus = bonusPool[bonusSelector].GetPooledObject();
        Bonus.transform.position = startPosition;
        Bonus.SetActive(true);
        
        NetworkObject netObj = Bonus.GetComponent<NetworkObject>();//get networkobject attached to prefab
        if (netObj != null && !netObj.IsSpawned && NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer)//spawn object if it has a networkobject,hasnt been spawned already, and in the scene there is a networkmanager/instance is the server
        {
            netObj.Spawn();//spawns on network
        }
    }
}

