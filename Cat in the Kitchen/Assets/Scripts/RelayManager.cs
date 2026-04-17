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
using System.Collections;
using System.Collections.Generic;



public class RelayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI joinCodeText;

    [SerializeField] private TMP_InputField joinCodeInputField;
    private bool isInitialized;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        isInitialized = true;
    }

    public async void StartRelay()
    {
        if (!isInitialized)
        {
            Debug.Log("Services not initialized yet");
            return;
        } 
        string joinCode = await StartHostWithRelay();
        joinCodeInputField.text = joinCode;
    }

    public async void JoinRelay()
    {
        await StartClientWithRelay(joinCodeInputField.text);
    }

    private async Task<string> StartHostWithRelay(int maxConnections = 4)
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
        RelayServerData relayServerData = allocation.ToRelayServerData("wss");
        NetworkManager.Singleton.GetComponent<UnityTransport>().UseWebSockets = true;
      NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
      
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        return NetworkManager.Singleton.StartHost() ? joinCode : null;
           
    }

    private async Task<bool> StartClientWithRelay(string joinCode)
    {
        JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
        RelayServerData relayServerData = joinAllocation.ToRelayServerData("wss");
        NetworkManager.Singleton.GetComponent<UnityTransport>().UseWebSockets = true;
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
        
        return !string.IsNullOrEmpty(joinCode) && NetworkManager.Singleton.StartClient(); 
    }
}
