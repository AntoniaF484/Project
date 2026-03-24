using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetworkStartUI : NetworkBehaviour
{
    float w = 200f, h = 40f;

    float x = 10f, y = 10f;
    [SerializeField] private TextMeshProUGUI playerCountText;
    
    private NetworkVariable<int> playerCount = new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone);

    void OnGUI()
    {

        if (NetworkManager.Singleton == null)
            return;

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUI.Button(new Rect(x, y, w, h), "Host")) NetworkManager.Singleton.StartHost();
            if (GUI.Button(new Rect(x, y + h + 10, w, h), "Client")) NetworkManager.Singleton.StartClient();
            if (GUI.Button(new Rect(x, y + 2 * (h + 10), w, h), "Server")) NetworkManager.Singleton.StartServer();

        }
    }
    void Update()
    {
        
       if (playerCountText!=null)
        playerCountText.text = playerCount.Value.ToString();
        if (!IsServer) return;
        
        if (NetworkManager.Singleton !=null)
        
        playerCount.Value=NetworkManager.Singleton.ConnectedClients.Count;
    }

}
