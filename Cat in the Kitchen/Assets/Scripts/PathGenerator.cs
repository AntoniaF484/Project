using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
   // public GameObject platform1;
    //public GameObject[] Path1Prefabs;
    
    private float[] platformWidths1;

    private float[] platformWidths2;

    public Transform generationPoint;
    public Transform generationPoint2;
 
    private int platformSelector1;
   private int platformSelector2;
    
    private float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;
    
private float platformWidth;
    private float minYPath1;
    public Transform maxYPointPath1;
    private float maxYpath1;
    private float Path1YChange;
    public float maxYchangePath1;
    
  public Transform maxYPointPath2;
    public Transform minYPointPath2;
    private float maxYpath2;
    private float minYpath2;
    private float Path2YChange;
    public float maxYchangePath2;
    public Vector3 path2Position;

    public ObjectPooler [] theObjectPoolsPath1;
    public ObjectPooler[] theObjectPoolsPath2;
    
    //test
    public GameObject Platform2;
  //  public Transform generationPoint2;
    public float distanceBetween2;
    private float platformWidth2;
    
    

    //public GameObject[] Path2Prefabs;

    // Start is called before the first frame update
        void Start()
        {
          //platformWidth = platform1.GetComponent<Renderer>().bounds.size.x;
           platformWidths1 = new float [theObjectPoolsPath1.Length];
           for (int i = 0; i < theObjectPoolsPath1.Length; i++)
           {
               platformWidths1[i] = theObjectPoolsPath1[i].pooledObject.GetComponent<Renderer>().bounds.size.x;
           }

           minYPath1 = transform.position.y;
           maxYpath1 = maxYPointPath1.position.y;
           
        platformWidths2 = new float [theObjectPoolsPath2.Length];
           for (int i = 0; i < theObjectPoolsPath2.Length; i++)
           {
               platformWidths2[i] = theObjectPoolsPath2[i].pooledObject.GetComponent<Renderer>().bounds.size.x;
           }
           
         path2Position = new Vector3(transform.position.x, maxYPointPath2.position.y, transform.position.z);
        // minYpath2 = minYPointPath2.position.y;
        // maxYpath2 = maxYPointPath2.position.y;

         // TEST platformWidth2 = Platform2.GetComponent<Renderer>().bounds.size.x;
        }
        
        

        // Update is called once per frame
        void Update()
        
        {
         GeneratePath1();
          GeneratePath2();
        }

     void GeneratePath1()
       {
            if (transform.position.x < generationPoint.position.x)
            {
                distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
                platformSelector1 = Random.Range(0, theObjectPoolsPath1.Length);
                Path1YChange = transform.position.y + Random.Range(-maxYchangePath1, maxYchangePath1);
                if (Path1YChange > maxYchangePath1)
                {
                    Path1YChange = maxYpath1;
                }
                else if (Path1YChange < minYPath1)
                {
                    Path1YChange = minYPath1;
                }
                transform.position = new Vector3(transform.position.x + (platformWidths1[platformSelector1]/2) + distanceBetween,
                    Path1YChange, transform.position.z);

                
                GameObject newPlatform = theObjectPoolsPath1 [platformSelector1].GetPooledObject();
                newPlatform.transform.position = transform.position;
                newPlatform.transform.rotation = transform.rotation;
                newPlatform.SetActive(true);
              
                transform.position = new Vector3(transform.position.x + (platformWidths1[platformSelector1]/2),
                    transform.position.y, transform.position.z);

            }
        }
       void GeneratePath2()
       
        {
            // if (transform.position.x < generationPoint2.position.x)
          //  {
           //     transform.position = new Vector3(transform.)
           // }



             platformWidths2 = new float [theObjectPoolsPath2.Length];
            for (int i = 0; i < theObjectPoolsPath2.Length; i++)
            {
                platformWidths2[i] = theObjectPoolsPath2[i].pooledObject.GetComponent<Renderer>().bounds.size.x;
            }
           // minYpath2 = minYPointPath2.position.y;
           // maxYpath2 = maxYPointPath2.position.y;
            if (path2Position.x < generationPoint2.position.x)
            {
                distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
                platformSelector2 = Random.Range(0, theObjectPoolsPath2.Length);
                Path2YChange = path2Position.y + Random.Range(-maxYchangePath2, maxYchangePath2);
                if (Path2YChange > maxYchangePath2)
                {
                    Path2YChange = maxYpath2;
                }
                else if (Path2YChange < minYpath2)
                {
                    Path2YChange = minYpath2;
                }
                path2Position = new Vector3(path2Position.x + (platformWidths2[platformSelector2]/2) + distanceBetween,
                    Path2YChange, path2Position.z);

                GameObject newPlatform = theObjectPoolsPath2 [platformSelector2].GetPooledObject();
                newPlatform.transform.position = transform.position;
                newPlatform.transform.rotation = transform.rotation;
                newPlatform.SetActive(true);

                transform.position = new Vector3(transform.position.x + (platformWidths2[platformSelector2]/2),
                    transform.position.y, transform.position.z);

            }
        }
}
