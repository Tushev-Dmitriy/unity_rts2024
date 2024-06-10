using UnityEngine;

public class Noise : MonoBehaviour
{
    public int mapWidth = 100;
    public int mapHeight = 100;
    public float scale = 20f;
    public float heightMultiplier = 5f;
    public float noiseThreshold = 0.5f;

    public GameObject objectPrefab1;
    public GameObject objectPrefab2;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float xCoord = (float)x / mapWidth * scale;
                float yCoord = (float)y / mapHeight * scale;
                float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

                Vector3 position = new Vector3(x, noiseValue * heightMultiplier, y);

                if (noiseValue > noiseThreshold)
                {
                    Instantiate(objectPrefab1, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(objectPrefab2, position, Quaternion.identity);
                }
            }
        }
    }
}