using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TerrainCollider))]
public class TerrainGenerator : MonoBehaviour
{
	//create variavles and set terrain width and height
	private TerrainData myTerrainData;
	[Tooltip("The size of the terrain")]
	public Vector3 worldSize = new Vector3(200, 50, 200);
	[Tooltip("Number of vertices along X and Z axes")]
	[Min(1)]
	public int resolution = 129;
	float[,] heightArray;


	void Start()
	{
		myTerrainData = gameObject.GetComponent<TerrainCollider>().terrainData;
		myTerrainData.size = worldSize;
		myTerrainData.heightmapResolution = resolution;

		heightArray = new float[resolution, resolution];

		Perlin();
		// Assign values from heightArray into the terrain object's heightmap
		myTerrainData.SetHeights(0, 0, heightArray);
	}


	void Update()
	{

	}
	//terrain
	void Perlin()
	{
		float x = 0, y = 0, value; 
		// Fill heightArray with Perlin-based values
		for (int i = 0; i < resolution; i++)
		{
			for (int j = 0; j < resolution; j++)
			{
				value = Mathf.PerlinNoise(x, y);
				heightArray[i, j] = value;
				x += 0.05f;
			}
			//rest x
			x = 0.0f;
			y += 0.02f;		
		}
	}
}
