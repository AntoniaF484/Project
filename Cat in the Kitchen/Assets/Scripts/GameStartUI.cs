using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;
public class GameStartUI : NetworkBehaviour
{
    public TMP_InputField nameText;
    
    private IndividualPlayerStats thisPlayersStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnReadyClicked()
    {
        var playerObj = NetworkManager.Singleton.LocalClient.PlayerObject;

        if (playerObj == null)
        {
            
            return;
        }

        var stats = playerObj.GetComponent<IndividualPlayerStats>();

        if (stats == null)
        {
            return;
        }

        stats.SetPlayerNameServerRpc(nameText.text);
        stats.SetReadyServerRpc(true);
        
        
        FindObjectOfType<GameManager>().PlayerReadyServerRpc();

        gameObject.SetActive(false);

    }
}
