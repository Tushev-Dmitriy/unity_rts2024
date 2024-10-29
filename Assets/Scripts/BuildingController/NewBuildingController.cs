using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBuildingController : MonoBehaviour
{
    public Material[] lineMaterials;
    public bool canBuild = false;
    public List<Collider> colliders = new List<Collider>();
    private List<Collider> GetColliders() { return colliders; }
    private LineRenderer lineRenderer;
    private int buildingMaxHealth;
    private int buildingHealh;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
    }

    private void Update()
    {
        if (GetColliders().Count != 0)
        {
            if (GetColliders().Count == 1)
            {
                if (colliders[0].tag == "Plane")
                {
                    SetMaterialToLine(2);
                    canBuild = true;
                } else
                {
                    SetMaterialToLine(1);
                    canBuild = false;
                }
            } else
            {
                SetMaterialToLine(1);
                canBuild = false;
            }
        } else
        {
            SetMaterialToLine(2);
            canBuild = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }

    public void SetMaterialToLine(int index)
    {
        lineRenderer.material = lineMaterials[index];
        
        if (index == 0)
        {
            lineRenderer.enabled = false;
        }
    }

    public void StartBuilding()
    {
        buildingHealh = 0;
        GetComponent<BuildingDataController>().buildingHealh = 0;
        buildingMaxHealth = GetComponent<BuildingDataController>().buildingMaxHealth;
        StartCoroutine(StartBuild());
    }

    IEnumerator StartBuild()
    {
        while (buildingHealh < buildingMaxHealth)
        {
            yield return new WaitForSeconds(1);
            buildingHealh += 25;
            GetComponent<BuildingDataController>().buildingHealh = buildingHealh;
        }

        SetMaterialToLine(0);
        Destroy(GetComponent<NewBuildingController>());
        yield break;
    }
}
