using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public GameObject Platform1;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;

    public GameObject[] Path1Prefabs;

    public GameObject[] Path2Prefabs;

    // Start is called before the first frame update
        void Start()
        {
            platformWidth = Platform1.GetComponent<Renderer>().bounds.size.x;
            Debug.Log("Platform Width: " + platformWidth);
        }

        // Update is called once per frame
        void Update()
        {
            while (transform.position.x < generationPoint.position.x)
            {
                transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween,
                    transform.position.y, transform.position.z);

                Instantiate(Platform1,transform.position, transform.rotation);
            }
        }
    
}
