
using TMPro;
using Unity.Netcode;
public class GameStartUI : NetworkBehaviour
{
    public TMP_InputField nameText;
    private bool hasClickedReady;

    public void OnReadyClicked() // when player presses ready button
    {
       if (hasClickedReady) return; 
       var playerObj = NetworkManager.Singleton.LocalClient.PlayerObject; // get the players player object from the network manager

        if (playerObj == null) // if the player object hasnt spawned yet, stop. 
        {
            
            return;
        }

        var stats = playerObj.GetComponent<IndividualPlayerStats>(); // get this players inputted stats (name and ready status in this case)

        if (stats == null)
        {
            return;
        }
        hasClickedReady = true;

        stats.SetPlayerNameServerRpc(nameText.text); // sets the players inputted name. Update network varialbe so everyone can see it
        stats.SetReadyServerRpc(true); // sets the player as ready
        
        
        FindObjectOfType<GameManager>().PlayerReadyServerRpc(); //notify game manager there is +1 ready player

        gameObject.SetActive(false); // hide ui for player once theyre ready

    }
}
