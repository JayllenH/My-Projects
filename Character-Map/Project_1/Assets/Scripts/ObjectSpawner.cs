using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectSpawn;
    void Start()
    {
        //loop for objecys
        for (int i = 0; i < 50; i++)
        {
            //random x and z
            float x = Random.Range(0,200);
            float z = Random.Range(0, 200);
            //gets height of terrain
            float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z));
            //create object
            Instantiate(objectSpawn, new Vector3(x, y, z), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    
}
