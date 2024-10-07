using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    [Header("Map settings")]
    public Camera mapCam;
    public TMP_InputField mapScaleText;
    public GameObject mapObj;
    public TMP_Dropdown mapEnemyCountDropdown;
    public GameObject mapObjects;

    [Header("Prefabs")]
    public GameObject stonePrefab;
    public GameObject treePrefab;

    private int mapSize;
    private int mapScale;
    private float objectDensity = 0.6f;

    public void SetupMapScale()
    {
        mapSize = int.Parse(mapScaleText.text);
        mapScale = Mathf.CeilToInt(Mathf.Sqrt(mapSize));
        mapCam.transform.position = new Vector3(0, mapScale, 0);

        if (mapSize < 2500)
        {
            mapScale = 50;
        } else if (mapSize > 10000)
        {
            mapScale = 100;
        }

        mapObj.transform.localScale = new Vector3(mapScale, 0.1f, mapScale);

        //GenerateObjects();
    }

    private void GenerateObjects()
    {
        float halfWidth = mapScale / 2;
        float halfHeight = mapScale / 2;
        float minX = -0.45f;
        float maxX = 0.45f;
        float minZ = -0.45f;
        float maxZ = 0.45f;

        for (int x = 0; x < mapScale; x++)
        {
            for (int z = 0; z < mapScale; z++)
            {
                float perlinValue = Mathf.PerlinNoise((x - halfWidth) / mapScale, (z - halfHeight) / mapScale);

                if (perlinValue > 0.42f && Random.value < objectDensity)
                {
                    Vector3 position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
                    GameObject stone = Instantiate(stonePrefab, mapObjects.transform.GetChild(0));
                    stone.transform.localPosition = position;
                    stone.transform.localScale = new Vector3(0.015f, 10f, 0.015f);
                }
                else if (perlinValue <= 0.42f && Random.value < objectDensity)
                {
                    Vector3 position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
                    GameObject tree = Instantiate(treePrefab, mapObjects.transform.GetChild(1));
                    tree.transform.localPosition = position;
                    tree.transform.localScale = new Vector3(0.015f, 10f, 0.015f);
                }
            }
        }
    }
}
