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
    public Transform minYPointPath1;
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
    public Vector3 path1Position;

    public ObjectPooler [] theObjectPoolsPath1;
    public ObjectPooler[] theObjectPoolsPath2;

    private FoodGenerator foodGenerator;
    
    //test
    //public GameObject Platform2;
  //  public Transform generationPoint2;
  //  public float distanceBetween2;
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

           minYPath1 = minYPointPath1.position.y;
           maxYpath1 = maxYPointPath1.position.y;
           
        platformWidths2 = new float [theObjectPoolsPath2.Length];
           for (int i = 0; i < theObjectPoolsPath2.Length; i++)
           {
               platformWidths2[i] = theObjectPoolsPath2[i].pooledObject.GetComponent<Renderer>().bounds.size.x;
           }
           
         path2Position = new Vector3(transform.position.x, minYpath2, transform.position.z);
         minYpath2 = minYPointPath2.position.y;
         maxYpath2 = maxYPointPath2.position.y;


         foodGenerator = FindObjectOfType<FoodGenerator>();

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
            if (path1Position.x < generationPoint.position.x)
            {
                distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
                platformSelector1 = Random.Range(0, theObjectPoolsPath1.Length);
                Path1YChange = path1Position.y + Random.Range(-maxYchangePath1, maxYchangePath1);
                if (Path1YChange > maxYchangePath1)
                {
                    Path1YChange = maxYpath1;
                }
                else if (Path1YChange < minYPath1)
                {
                    Path1YChange = minYPath1;
                }
                path1Position = new Vector3(path1Position.x + (platformWidths1[platformSelector1]/2) + distanceBetween,
                    Path1YChange, path1Position.z);

                
                GameObject newPlatform = theObjectPoolsPath1 [platformSelector1].GetPooledObject();
                newPlatform.transform.position = path1Position;
                newPlatform.transform.rotation = transform.rotation;
                newPlatform.SetActive(true);
                
              foodGenerator.SpawnFood(new Vector3(path1Position.x,path1Position.y+3f,path1Position.z));
                
                path1Position = new Vector3(path1Position.x + (platformWidths1[platformSelector1]/2),
                    path1Position.y, path1Position.z);

            }
        }
       void GeneratePath2()
       
        {
            // if (transform.position.x < generationPoint2.position.x)
          //  {
           //     transform.position = new Vector3(transform.)
           // }



         /*    platformWidths2 = new float [theObjectPoolsPath2.Length];
            for (int i = 0; i < theObjectPoolsPath2.Length; i++)
            {
                platformWidths2[i] = theObjectPoolsPath2[i].pooledObject.GetComponent<Renderer>().bounds.size.x;
            }*/
           // minYpath2 = minYPointPath2.position.y;
           // maxYpath2 = maxYPointPath2.position.y;
           if (path2Position.x < generationPoint2.position.x)
           {
               distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
               platformSelector2 = Random.Range(0, theObjectPoolsPath2.Length);
               Path2YChange = path2Position.y + Random.Range(-maxYchangePath2, maxYchangePath2);
               if (Path2YChange > maxYpath2)
               {
                   Path2YChange = maxYpath2;
               }

               else if (Path2YChange < minYpath2)
               {
                   Path2YChange = minYpath2;
               }


               path2Position = new Vector3(path2Position.x + (platformWidths2[platformSelector2] / 2) + distanceBetween,
                   Path2YChange, path2Position.z);

               GameObject newPlatform = theObjectPoolsPath2[platformSelector2].GetPooledObject();
               newPlatform.transform.position = path2Position;
               newPlatform.transform.rotation = transform.rotation;
               newPlatform.SetActive(true);

               int RandomNumber = Random.Range(0, 100);
               if (RandomNumber < 90)
               {

               foodGenerator.SpawnFood(new Vector3(path2Position.x, path2Position.y + 3f, path2Position.z));
                }

           path2Position = new Vector3(path2Position.x + (platformWidths2[platformSelector2]/2),
                    path2Position.y, path2Position.z);

            }
        }
}
