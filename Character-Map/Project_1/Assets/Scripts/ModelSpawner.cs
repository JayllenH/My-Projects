using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject modelSpawner;

    void Start()
    {
        //x and z float to stay in a line
        float x = 0;
        float z = 100;
        //find they scale
        float xzScale = Gaussian(2.5f, 0.2f);
        for (int i =0; i<9; i++)
        {
            //find height
            float y = Terrain.activeTerrain.SampleHeight(new Vector3(x,0,z));
            //create object
            Instantiate(modelSpawner, new Vector3(x, y, z), Quaternion.identity);
            //get Y scale
            float yScale = Gaussian(2.4f, 0.9f);
            //change Y values
            modelSpawner.transform.localScale = new Vector3(xzScale, yScale, xzScale);
            //move x coord
            x += 20;
        }
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // gaussian method
    float Gaussian(float mean, float stdDev)
    {
        //random float in range of terrain
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);
        //gaussian equation
        float gaussValue1 =
                 Mathf.Sqrt(-2.0f * Mathf.Log(val1)) *
                 Mathf.Sin(2.0f * Mathf.PI * val2);
        //return the float
        return mean + stdDev * gaussValue1;
    }

  
   
}
