using UnityEngine;
using Unity.Netcode;

public class PlayerSetup : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        Camera cam = Camera.main; 
        if (cam != null)
        {
            FollowPlayer follow = cam.GetComponent<FollowPlayer>();
            if (follow != null)
            {
                follow.SetTarget(transform); 
            }
        }
    }
}