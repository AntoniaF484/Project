using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpManager : MonoBehaviour
{


    private GameManager gameManager;
    private PathGenerator pathGenerator;
    private DetectCollisions detectCollisions;

   private float difficulty;




    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
  

    public void ActivateDoubleScore(IndividualPlayerStats player, float time)
    {
        StartCoroutine(DoubleScore(player, time));
    }

    IEnumerator DoubleScore(IndividualPlayerStats player, float duration)
    {
        player.SetScoreMultiplyerServerRpc(2);
        yield return new WaitForSeconds(duration);
        player.SetScoreMultiplyerServerRpc(1);
    }

    public void ActivateTakeOtherLives(IndividualPlayerStats triggerPlayer)
    {
        foreach (IndividualPlayerStats player in FindObjectsOfType<IndividualPlayerStats>())
        {
            if (player.OwnerClientId == triggerPlayer.OwnerClientId) continue;
            player.lives.Value -= 1;
        }
    }
    
   
}
