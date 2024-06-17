using System;
using System.Collections;
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

    public GameObject startCanvas;

    public float width; 
    public float height; 
    public float scale = 20f; 
    public float objectDensity = 0.1f;

    private float f;
    private float sqrt;

    public void StartGame()
    {
        f = float.Parse(inputField.text);
        if (f >= 2500 && f <= 10000)
        {
            startCanvas.SetActive(false);
            SetMapScale();
            GenerateObjects();
            StartCoroutine(GenerateBuilds());
        }
    }

    private void GenerateObjects()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float perlinValue = Mathf.PerlinNoise(x / scale, z / scale);

                if (perlinValue > 0.6f && UnityEngine.Random.value < objectDensity)
                {
                    Vector3 position = new Vector3(x, 0, z);
                    Instantiate(stonePrefab, position, Quaternion.identity, transform);
                }
                else if (perlinValue <= 0.6f && UnityEngine.Random.value < objectDensity)
                {
                    Vector3 position = new Vector3(x, 0, z);
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
            Vector3 pos = new Vector3(UnityEngine.Random.Range(0, sqrt), 0.97f, UnityEngine.Random.Range(0, sqrt));
            GameObject tempGO = Instantiate(townCenter, pos, Quaternion.identity);
            tempGO.name = $"town center {i+1}";
        }
    }

    private void SetMapScale()
    {
        sqrt = Mathf.Sqrt(f);
        plane.transform.localScale = new Vector3(Mathf.Round(sqrt), plane.transform.localScale.y, Mathf.Round(sqrt));
        width = Mathf.Round(sqrt - 3);
        height = Mathf.Round(sqrt - 3);
    }
}