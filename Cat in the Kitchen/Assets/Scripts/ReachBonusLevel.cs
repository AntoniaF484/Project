using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextscene : MonoBehaviour
{
    public string BonusLevel;
    public float bonusTime;
    
    

    public string MyGame; //Original scene name
    private GameObject player;

    public bool returnFromBonusLevel;
    
 
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){ 
            
            SceneManager.LoadScene(BonusLevel);

           player = other.gameObject;
           PlayerPrefs.SetString("lastLoadedScene",SceneManager.GetActiveScene().name);
           
        }
    }

  
}
