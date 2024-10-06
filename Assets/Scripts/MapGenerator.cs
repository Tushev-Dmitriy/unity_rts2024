using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    [Header("Map settings")]
    public Camera mapCam;
    public GameObject baseObject;
    public TMP_InputField mapScaleText;
    public GameObject mapObj;
    public TMP_Dropdown mapEnemyCountDropdown;
    public GameObject mapObjects;
    public GameObject userBase;
    public GameObject enemyBase;
    private List<Vector3> unitSpawnPos = new List<Vector3>() 
    {
        new Vector3(13, -8, 23), new Vector3(6, -8, 23), new Vector3(-1, -8, 23),
        new Vector3(-8, -8, 23), new Vector3(-15, -8, 23)
    };
    private List<Vector3> basePositions = new List<Vector3>();
    private float minDistanceBetweenTowns = 0.1f;

    [Header("Prefabs")]
    public GameObject stonePrefab;
    public GameObject treePrefab;
    public GameObject townCenterPrefab;
    public GameObject[] units;

    private int mapSize;
    private int mapScale;
    private float objectDensity = 0.6f;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

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

                if (perlinValue > 0.43f && Random.value < objectDensity)
                {
                    Vector3 position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
                    GameObject stone = Instantiate(stonePrefab, mapObjects.transform.GetChild(0));
                    stone.transform.localPosition = position;
                    stone.transform.localScale = new Vector3(0.015f, 10f, 0.015f);
                }
                else if (perlinValue <= 0.43f && Random.value < objectDensity)
                {
                    Vector3 position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
                    GameObject tree = Instantiate(treePrefab, mapObjects.transform.GetChild(1));
                    tree.transform.localPosition = position;
                    tree.transform.localScale = new Vector3(0.015f, 10f, 0.015f);
                }
            }
        }

        StartCoroutine(GenerateBuilds());
    }

    IEnumerator GenerateBuilds()
    {
        yield return new WaitForSeconds(0.05f);

        int baseCount = mapEnemyCountDropdown.value + 2;
        float radius = 5f;

        for (int i = 0; i < baseCount; i++)
        {
            GameObject townCenter;

            if (i == 0)
            {
                townCenter = Instantiate(townCenterPrefab, userBase.transform);
                townCenter.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.1f);
                cam.transform.SetParent(townCenter.transform);
                cam.transform.localPosition = new Vector3(0, 80, 75);
                cam.transform.localRotation = Quaternion.Euler(40, 180, 0);
            } else
            {
                townCenter = Instantiate(townCenterPrefab, enemyBase.transform);
                townCenter.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.1f);
            }

            townCenter.transform.localScale = new Vector3(0.0023f, 1.15f, 0.0023f);

            Vector3 posR;
            do
            {
                posR = new Vector3(Random.Range(-0.4f, 0.4f), 10,
                    Random.Range(-0.4f, 0.4f));
            } while (!IsPositionValid(posR));

            basePositions.Add(posR);
            townCenter.transform.localPosition = posR;

            Collider[] colliders = Physics.OverlapSphere(townCenter.transform.position, radius);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "MapObject")
                {
                    Destroy(collider.gameObject);
                }
            }

            cam.transform.SetParent(baseObject.transform);
        }

        UnitSpawn();
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (var townPos in basePositions)
        {
            if (Vector3.Distance(townPos, position) < minDistanceBetweenTowns)
            {
                return false;
            }
        }
        return true;
    }

    private void UnitSpawn()
    {
        UserBaseUnitSpawn();
        EnemyBaseUnitSpawn();
    }

    private void UserBaseUnitSpawn()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject villagerUnit = Instantiate(units[0], userBase.transform.GetChild(0).transform);
            villagerUnit.transform.localRotation = new Quaternion(0, 180, 0, 0);
            villagerUnit.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            villagerUnit.transform.localPosition = unitSpawnPos[i];
        }

        for (int j = 0; j < 2; j++)
        {
            GameObject archerUnit = Instantiate(units[1], userBase.transform.GetChild(0).transform);
            archerUnit.transform.localRotation = new Quaternion(0, 180, 0, 0);
            archerUnit.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            archerUnit.transform.localPosition = unitSpawnPos[j + 3];
        }
    }

    private void EnemyBaseUnitSpawn()
    {
        //конфигурация
    }
}
