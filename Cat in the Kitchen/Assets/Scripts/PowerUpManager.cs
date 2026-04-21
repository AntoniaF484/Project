using System.Collections;

using UnityEngine;


public class PowerUpManager : MonoBehaviour
{

    private DetectCollisions detectCollisions;
 
    public void ActivateDoubleScore(IndividualPlayerStats player, float time) // if the player collides with the double score powerup, start double score coroutine
    {
        StartCoroutine(DoubleScore(player, time));
    }

    IEnumerator DoubleScore(IndividualPlayerStats player, float duration) // In player stats players score multiplier is doubled for the defined duration, and then ruturns to 1
    {
        player.SetScoreMultiplyerServerRpc(2);
        yield return new WaitForSeconds(duration);
        player.SetScoreMultiplyerServerRpc(1);
    }

    public void ActivateTakeOtherLives(IndividualPlayerStats triggerPlayer)// take a life in player stats from each player other than the triggered player, who collided with the life powerup
    {
        foreach (IndividualPlayerStats player in FindObjectsOfType<IndividualPlayerStats>())
        {
            if (player.OwnerClientId == triggerPlayer.OwnerClientId) continue;
            player.lives.Value -= 1;
        }
    }
    
   
}
