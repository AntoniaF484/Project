using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public GameObject platform1;
    public GameObject[] Path1Prefabs;
    private float[] platformWidths;
    public Transform generationPoint;
    private int platformSelector;
    private float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

    private float platformWidth;

    public ObjectPooler theObjectPool;
    

    //public GameObject[] Path2Prefabs;

    // Start is called before the first frame update
        void Start()
        {
           // platformWidth = platform1.GetComponent<Renderer>().bounds.size.x;
           platformWidths = new float [Path1Prefabs.Length];
           for (int i = 0; i < Path1Prefabs.Length; i++)
           {
               platformWidths[i] = Path1Prefabs[i].GetComponent<Renderer>().bounds.size.x;
           }

        }

        // Update is called once per frame
        void Update()
        
        {
         
            if (transform.position.x < generationPoint.position.x)
            {
                distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
                platformSelector = Random.Range(0, Path1Prefabs.Length);
                transform.position = new Vector3(transform.position.x + platformWidths[platformSelector] + distanceBetween,
                    transform.position.y, transform.position.z);

                //platformSelector = Random.Range(0, Path1Prefabs.Length);

              Instantiate(Path1Prefabs[platformSelector],transform.position, transform.rotation);
             // GameObject newPlatform = theObjectPool.GetPooledObject();
              //newPlatform.transform.position = transform.position;
              //newPlatform.transform.rotation = transform.rotation;
              //newPlatform.SetActive(true);

            }
        }
    
}
