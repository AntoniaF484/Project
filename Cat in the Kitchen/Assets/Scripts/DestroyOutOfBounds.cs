using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public GameObject DestructionPoint;
    // Start is called before the first frame update
    void Start()
    {
        DestructionPoint = GameObject.Find("DestructionPoint");
    }

    // Update is called once per frame
    void Update() //Deactivates objects once they pass the destruction point (set as a child of the main camera)
    {
        if (transform.position.x<DestructionPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
    }
}
