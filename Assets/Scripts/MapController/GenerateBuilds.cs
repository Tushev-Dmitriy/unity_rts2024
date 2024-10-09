using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateBuilds : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject townCenterPrefab;

    [Header("Obj in inspector")]
    public GameObject baseObject;
    public GameObject userBase;
    public GameObject enemyBase;
    public TMP_Dropdown mapEnemyCountDropdown;

    private Camera cam;
    private List<Vector3> basePositions = new List<Vector3>();
    private float minDistanceBetweenTowns = 0.2f;

    private GameObject tempObj;

    private void Start()
    {
        cam = Camera.main;
    }

    public void GenerateBuildsOnMap()
    {
        int baseCount = mapEnemyCountDropdown.value + 2;
        float radius = 1f;

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
            }
            else
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

            Collider[] objectsInRange = Physics.OverlapSphere(townCenter.transform.position, 0.75f);
            foreach (Collider col in objectsInRange)
            {
                if (col.CompareTag("MapObject"))
                {
                    Destroy(col.gameObject);
                }
            }

            cam.transform.SetParent(baseObject.transform);
        } 
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
}
