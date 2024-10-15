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
    private float objectDensity = 0.4f;

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

        GenerateObjects();
    }

    private void GenerateObjects()
    {
        float halfWidth = mapScale / 2f - 2f;
        float halfHeight = mapScale / 2f - 2f;

        int totalObjects = Mathf.FloorToInt(mapScale * mapScale * objectDensity);

        for (int i = 0; i < totalObjects; i++)
        {
            float randomX = Random.Range(-halfWidth, halfWidth);
            float randomZ = Random.Range(-halfHeight, halfHeight);
            Vector3 position = new Vector3(randomX, 0, randomZ);

            float perlinValue = Mathf.PerlinNoise((randomX + halfWidth) / mapScale, (randomZ + halfHeight) / mapScale);

            GameObject objToSpawn;

            if (perlinValue > 0.37f)
            {
                objToSpawn = stonePrefab;
            }
            else
            {
                objToSpawn = treePrefab;
            }

            GameObject spawnedObj = Instantiate(objToSpawn, position, Quaternion.identity);

            if (objToSpawn == stonePrefab)
            {
                spawnedObj.transform.SetParent(mapObjects.transform.GetChild(0));
            }
            else
            {
                spawnedObj.transform.SetParent(mapObjects.transform.GetChild(1));
            }

            spawnedObj.transform.localScale = new Vector3(0.015f, 10f, 0.015f);
        }
    }

}
