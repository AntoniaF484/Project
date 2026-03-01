using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Netcode;
using UnityEngine;

public class NetworkStartUI : MonoBehaviour
{
    float w = 200f, h = 40f;

    float x = 10f, y = 10f;
    void OnGUI()
    {

  

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
    {
        if (GUI.Button(new Rect(x, y, w, h), "Host")) NetworkManager.Singleton.StartHost();
        if (GUI.Button(new Rect(x, y + h + 10, w, h), "Client")) NetworkManager.Singleton.StartClient();
        if (GUI.Button(new Rect(x, y + 2 * (h + 10), w, h), "Server")) NetworkManager.Singleton.StartServer();

    }
}

}
