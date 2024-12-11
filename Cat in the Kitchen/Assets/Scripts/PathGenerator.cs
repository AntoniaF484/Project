using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{    
    
    public Transform generationPoint;//Point in the game at which paths and attached objects are generated (attached to camera position)
    private FoodObstaclePowerupGenerator objGenerator;
    private GameManager gameManager;
    
//Path 1 Variables
   
       public ObjectPooler [] theObjectPoolsPath1;
       private float[] platformWidths1;
       private int platformSelector1;
   
   //Distance Between generated platforms (x coordinates) 
       public float distanceBetweenMinPath1;
       public float distanceBetweenMaxPath1;
       private float distanceBetweenPath1;
       
       
    // Height of generated platforms (y coordinates)
        public Transform maxYPointPath1;
        public Transform minYPointPath1;
        public float maxYchangePath1;
        private float maxYpath1;
        private float minYPath1;
        private float Path1YChange;
    
    //Position of Path 1 platform
        private Vector3 path1Position;
        
//Path 2 Variables
       public ObjectPooler[] theObjectPoolsPath2;
       private float[] platformWidths2;
       private int platformSelector2;
    
   //Distance Between generated platforms (x coordinates)
        public float distanceBetweenMinPath2;
        public float distanceBetweenMaxPath2; 
        private float distanceBetweenPath2;
        
    // Height of generated platforms (y coordinates)
        public Transform maxYPointPath2;
        public Transform minYPointPath2;
        public float maxYchangePath2;
        private float maxYpath2;
        private float minYpath2;
        private float Path2YChange;
        
    //Position of Path 2 platforms
        private Vector3 path2Position;


    // Start is called before the first frame update
        void Start()
        {
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


         objGenerator = FindObjectOfType<FoodObstaclePowerupGenerator>();
         
        }

    public void GeneratePath1()
       {
           
            if (path1Position.x < generationPoint.position.x)
            {
                distanceBetweenPath1 = Random.Range(distanceBetweenMinPath1, distanceBetweenMaxPath1);
                platformSelector1 = Random.Range(0, theObjectPoolsPath1.Length); //Picking a platform from the pool
                
                Path1YChange = path1Position.y + Random.Range(-maxYchangePath1, maxYchangePath1); // setting height of platform
                if (Path1YChange > maxYchangePath1)
                {
                    Path1YChange = maxYpath1;
                }
                else if (Path1YChange < minYPath1)
                {
                    Path1YChange = minYPath1;
                }
                path1Position = new Vector3(path1Position.x + (platformWidths1[platformSelector1]/2) + distanceBetweenPath1,
                    Path1YChange, path1Position.z); //position at which platform will appear

                
                GameObject newPlatform = theObjectPoolsPath1 [platformSelector1].GetPooledObject();
                newPlatform.transform.position = path1Position;
                newPlatform.transform.rotation = transform.rotation; 
                newPlatform.SetActive(true); //setting platform as active in above named position
                
               int RandomNumberPath1 = Random.Range(0, 100);
               if (RandomNumberPath1 > 60)
                {
                    objGenerator.SpawnFood(new Vector3(
                        path1Position.x + Random.Range(-(platformWidths1[platformSelector1] / 2),
                            (platformWidths1[platformSelector1] / 2)), path1Position.y + 3f, path1Position.z)); // Generates food on the generated platform
                }

               else if (RandomNumberPath1 > 40)
                {
                    objGenerator.SpawnObstacle(new Vector3(
                        path1Position.x + Random.Range(-(platformWidths1[platformSelector1] / 2),
                            (platformWidths1[platformSelector1] / 2)), path1Position.y + 3f, path1Position.z)); // Generates obstacles on the generated platform
                }

                else
                {
                    objGenerator.SpawnPowerUp(new Vector3(
                        path1Position.x + platformWidths1[platformSelector1] / 2+ distanceBetweenPath1/2, path1Position.y + 5f,
                        path1Position.z));
                }

                path1Position = new Vector3(path1Position.x + (platformWidths1[platformSelector1]/2),
                       path1Position.y, path1Position.z); //new platform position
               

                

            }
        }
      public void GeneratePath2()
       
        {
        
           if (path2Position.x < generationPoint.position.x)
           {
               distanceBetweenPath2 = Random.Range(distanceBetweenMinPath2, distanceBetweenMaxPath2);
               platformSelector2 = Random.Range(0, theObjectPoolsPath2.Length); //picking a platform from the pool
               Path2YChange = path2Position.y + Random.Range(-maxYchangePath2, maxYchangePath2); //setting platform height
               if (Path2YChange > maxYpath2)
               {
                   Path2YChange = maxYpath2;
               }

               else if (Path2YChange < minYpath2)
               {
                   Path2YChange = minYpath2;
               }


               path2Position = new Vector3(path2Position.x + (platformWidths2[platformSelector2] / 2) + distanceBetweenPath2,
                   Path2YChange, path2Position.z); // position at which platform will appear

               GameObject newPlatform = theObjectPoolsPath2[platformSelector2].GetPooledObject();
               newPlatform.transform.position = path2Position;
               newPlatform.transform.rotation = transform.rotation;
               newPlatform.SetActive(true); // setting platform as active in above named position

               int RandomNumber = Random.Range(0, 100);
               if (RandomNumber > 50) 
               {
                objGenerator.SpawnFood(new Vector3(path2Position.x+ Random.Range(-(platformWidths2[platformSelector2]/2), 
                    (platformWidths2[platformSelector2]/2)), path2Position.y + 3f, path2Position.z)); //generating food on the platform just generated
               }
               else if (RandomNumber > 10) 
               
               {
                   objGenerator.SpawnPowerUp(new Vector3(
                       path2Position.x + platformWidths2[platformSelector2] / 2+ distanceBetweenPath2/2, path2Position.y + 5f,
                       path2Position.z));
                   
               }
               else  
               {
                   objGenerator.SpawnObstacle(new Vector3(path2Position.x+ Random.Range(-(platformWidths2[platformSelector2]/2), 
                       (platformWidths2[platformSelector2]/2)), path2Position.y + 3f, path2Position.z)); // generating obstacles on the platform just generated
               }
               
              

            path2Position = new Vector3(path2Position.x + (platformWidths2[platformSelector2]/2),
                    path2Position.y, path2Position.z); // new platform position

            }
        }
}
