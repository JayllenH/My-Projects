using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hordeSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hordeSpawner;
    void Start()
    {
        //loop for objects
        for(int i =0; i<100; i++)
        {
            //random float
            float random = Random.Range(0f, 1f);
            //if statements to make somespawn closer than others
            if (random <= 0.5f)
            {
                //random x and z coord
                float x = Random.Range(0, 180);
                float z = Random.Range(0, 100);
                //create object
                Instantiate(hordeSpawner, new Vector3(x, Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z)), z), Quaternion.identity);
            }
            else
            {
                //random x and z coord
                float x = Random.Range(75, 180);
                float z = Random.Range(75, 100);
                //create object
                Instantiate(hordeSpawner, new Vector3(x, Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z)), z), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
