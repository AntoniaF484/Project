using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;

public class RelayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI joinCodeText;

    [SerializeField] private TMP_InputField joinCodeInputField;
    private bool isInitialized;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private async void Start() // initialises unity services, logs player in anonymously 
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        isInitialized = true;
    }

    public async void StartRelay() // start host and print join code
    {
        if (!isInitialized)
        {
            return;
        } 
        string joinCode = await StartHostWithRelay();
        joinCodeInputField.text = joinCode;
    }

    public async void JoinRelay() // once join button is pressed, uses join code put in by user
    {
        await StartClientWithRelay(joinCodeInputField.text);
    }

    private async Task<string> StartHostWithRelay(int maxConnections = 4) // hosts side
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);// creates relay allocation for max players
        RelayServerData relayServerData = allocation.ToRelayServerData("wss");
        NetworkManager.Singleton.GetComponent<UnityTransport>().UseWebSockets = true;
      NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
      
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId); // get join code for other players
        return NetworkManager.Singleton.StartHost() ? joinCode : null;
           
    }

    private async Task<bool> StartClientWithRelay(string joinCode) // clients side
    {
        JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);// join existing relay allocation with the join code
        RelayServerData relayServerData = joinAllocation.ToRelayServerData("wss");
        NetworkManager.Singleton.GetComponent<UnityTransport>().UseWebSockets = true;
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
        
        return !string.IsNullOrEmpty(joinCode) && NetworkManager.Singleton.StartClient(); // check join code and start connection
    }
}
