using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Noise : MonoBehaviour
{
    public GameObject plane;
    public TMP_InputField inputField;
    public TMP_Dropdown dropdown;
    public GameObject stonePrefab;
    public GameObject treePrefab;
    public GameObject townCenter;
    public GameObject[] units;
    public GameObject startCanvas;
    public Material[] materials;
    public float width;
    public float height;
    public float scale = 20f;
    public float objectDensity = 0.1f;
    public float minDistanceBetweenTowns = 45f;
    public UnitSelection unitSelection;

    private float f;
    private float sqrt;
    private List<Vector3> townPositions = new List<Vector3>();
    private Vector3[] posToUnits = new Vector3[5] {new Vector3(12, -8, 23), new Vector3(5, -8, 23), new Vector3(-2, -8, 23),
                                                   new Vector3(-9, -8, 23), new Vector3(-16, -8, 23)};

    public void StartGame()
    {
        f = float.Parse(inputField.text);
        if (f >= 2500 && f <= 10000)
        {
            startCanvas.SetActive(false);
            SetMapScale();
            GenerateObjects();
            StartCoroutine(GenerateBuilds());
            GetComponent<UnitController>().isGame = true;
        }
    }

    private void GenerateObjects()
    {
        float halfWidth = width / 2;
        float halfHeight = height / 2;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float perlinValue = Mathf.PerlinNoise((x - halfWidth) / scale, (z - halfHeight) / scale);

                if (perlinValue > 0.6f && UnityEngine.Random.value < objectDensity)
                {
                    Vector3 position = new Vector3(x - halfWidth, 0, z - halfHeight);
                    Instantiate(stonePrefab, position, Quaternion.identity, transform);
                }
                else if (perlinValue <= 0.6f && UnityEngine.Random.value < objectDensity)
                {
                    Vector3 position = new Vector3(x - halfWidth, 0, z - halfHeight);
                    Instantiate(treePrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }

    IEnumerator GenerateBuilds()
    {
        yield return new WaitForSeconds(0.05f);

        int countOfEnemies = dropdown.value + 1;
        for (int i = 0; i < countOfEnemies; i++)
        {
            Vector3 posR;
            do
            {
                posR = new Vector3(
                    UnityEngine.Random.Range(-sqrt / 2 + 2, sqrt / 2 - 2),
                    0.97f,
                    UnityEngine.Random.Range(-sqrt / 2 + 2, sqrt / 2 - 2)
                );
            } while (!IsPositionValid(posR));

            townPositions.Add(posR);

            Vector3 pos = posR;
            GameObject tempGO = Instantiate(townCenter, pos, Quaternion.identity);
            tempGO.name = $"town center {i + 1}";

            if (i == 0)
            {
                tempGO.transform.GetChild(2).GetComponent<Renderer>().material = materials[0];
                Camera cam = Camera.main;
                cam.transform.SetParent(tempGO.transform);
                cam.transform.localPosition = new Vector3(3.4f, 95, -90);
                cam.transform.parent = null;
                cam.transform.position = new Vector3(cam.transform.position.x, 12, cam.transform.position.z);
                unitSelection.unitsInTownCenter = tempGO.transform.GetChild(1).gameObject;
            }
            else
            {
                tempGO.transform.GetChild(2).GetComponent<Renderer>().material = materials[1];
            }

            Collider[] objectsInRange = Physics.OverlapSphere(tempGO.transform.position, 10f);
            foreach (Collider col in objectsInRange)
            {
                if (col.CompareTag("MapResource"))
                {
                    Destroy(col.gameObject);
                }
            }

            GameObject tempParent = GameObject.Find($"town center {i + 1}/Units");

            for (int j = 0; j < 3; j++)
            {
                GameObject tempUnit = Instantiate(units[0]);
                tempUnit.transform.SetParent(tempParent.transform);
                tempUnit.transform.localPosition = posToUnits[j];
                tempUnit.transform.localScale = Vector3.one;
            }

            for (int k = 3; k < 5; k++)
            {
                GameObject tempUnit = Instantiate(units[1]);
                tempUnit.transform.SetParent(tempParent.transform);
                tempUnit.transform.localPosition = posToUnits[k];
                tempUnit.transform.localScale = Vector3.one;
            }
        }
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (var townPos in townPositions)
        {
            if (Vector3.Distance(townPos, position) < minDistanceBetweenTowns)
            {
                return false;
            }
        }
        return true;
    }


    private void SetMapScale()
    {
        sqrt = Mathf.Sqrt(f);
        plane.transform.localScale = new Vector3(Mathf.Round(sqrt), plane.transform.localScale.y, Mathf.Round(sqrt));
        width = Mathf.Round(sqrt - 3);
        height = Mathf.Round(sqrt - 3);
    }
}
