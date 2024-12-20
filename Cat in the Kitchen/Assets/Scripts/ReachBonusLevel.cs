using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextscene : MonoBehaviour
{
    public string BonusLevel;
 
    private GameObject player;

    void OnTriggerEnter(Collider other) // on collision with bonus level powerup, load bonus level
    {
        if(other.CompareTag("Player")){ 
            
            SceneManager.LoadScene(BonusLevel);

           player = other.gameObject;
           
           
        }
    }

  
}
