using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public GameObject platform1;
    //public GameObject[] Path1Prefabs;
    
    private float[] platformWidths;
    public Transform generationPoint;
    private int platformSelector;
    private float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;
    private float platformWidth;
    private float minYPath1;
    public Transform maxYPointPath1;
    private float maxYpath1;
    private float Path1YChange;
    public float maxYchangePath1;

    public ObjectPooler [] theObjectPools;
    

    //public GameObject[] Path2Prefabs;

    // Start is called before the first frame update
        void Start()
        {
           // platformWidth = platform1.GetComponent<Renderer>().bounds.size.x;
           platformWidths = new float [theObjectPools.Length];
           for (int i = 0; i < theObjectPools.Length; i++)
           {
               platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<Renderer>().bounds.size.x;
           }

           minYPath1 = transform.position.y;
           maxYpath1 = maxYPointPath1.position.y;
        }

        // Update is called once per frame
        void Update()
        
        {
         
            if (transform.position.x < generationPoint.position.x)
            {
                distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
                platformSelector = Random.Range(0, theObjectPools.Length);
                Path1YChange = transform.position.y + Random.Range(-maxYchangePath1, maxYchangePath1);
               if (Path1YChange > maxYchangePath1)
                {
                    Path1YChange = maxYpath1;
                }
               else if (Path1YChange < minYPath1)
                {
                    Path1YChange = minYPath1;
                }
                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector]/2) + distanceBetween,
                    Path1YChange, transform.position.z);

                //platformSelector = Random.Range(0, Path1Prefabs.Length);

              //Instantiate(theObjectPools[platformSelector],transform.position, transform.rotation);
             GameObject newPlatform = theObjectPools [platformSelector].GetPooledObject();
              newPlatform.transform.position = transform.position;
              newPlatform.transform.rotation = transform.rotation;
              newPlatform.SetActive(true);
              
              transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector]/2),
                  transform.position.y, transform.position.z);

            }
        }
    
}
