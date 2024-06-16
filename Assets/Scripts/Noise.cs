using UnityEngine;

public class Noise : MonoBehaviour
{
    public GameObject stonePrefab; 
    public GameObject treePrefab;

    public int width = 100; 
    public int height = 100; 
    public float scale = 20f; 
    public float objectDensity = 0.1f; 

    void Start()
    {
        GenerateObjects();
    }

    void GenerateObjects()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float perlinValue = Mathf.PerlinNoise(x / scale, z / scale);

                if (perlinValue > 0.5f && Random.value < objectDensity)
                {
                    Vector3 position = new Vector3(x, 0, z);
                    Instantiate(stonePrefab, position, Quaternion.identity, transform);
                }
                else if (perlinValue <= 0.5f && Random.value < objectDensity)
                {
                    Vector3 position = new Vector3(x, 0, z);
                    Instantiate(treePrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }
}