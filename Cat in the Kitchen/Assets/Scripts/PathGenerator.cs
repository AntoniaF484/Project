using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public GameObject platform1;
    public Transform generationPoint;
    private float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

    private float platformWidth;

    public ObjectPooler theObjectPool;
   

   // public GameObject[] Path1Prefabs;

    //public GameObject[] Path2Prefabs;

    // Start is called before the first frame update
        void Start()
        {
            platformWidth = platform1.GetComponent<Renderer>().bounds.size.x;
      
        }

        // Update is called once per frame
        void Update()
        
        {
         
            if (transform.position.x < generationPoint.position.x)
            {
                distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
                transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween,
                    transform.position.y, transform.position.z);

              //  Instantiate(platform1,transform.position, transform.rotation);
              GameObject newPlatform = theObjectPool.GetPooledObject();
              newPlatform.transform.position = transform.position;
              newPlatform.transform.rotation = transform.rotation;
              newPlatform.SetActive(true);

            }
        }
    
}
