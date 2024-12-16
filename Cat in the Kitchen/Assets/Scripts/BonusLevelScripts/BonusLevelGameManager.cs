using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusLevelGameManager : MonoBehaviour
{

    public float bonusTimeCountdown;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bonusTimeCountdown -= Time.deltaTime;
     /*  if (bonusTimeCountdown <= 0)
        {
            SceneManager.LoadScene("MyGame");
        }*/
    }
}
