using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 offset = new Vector3(0,22,-28);
        public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       float transformPositionx = player.transform.position.x + offset.x;
transform.position = new Vector3 (transformPositionx,22,-28);
    }
}
