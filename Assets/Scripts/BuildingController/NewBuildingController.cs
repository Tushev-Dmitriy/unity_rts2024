using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBuildingController : MonoBehaviour
{
    public Material[] lineMaterials;
    public bool canBuild = false;
    private LineRenderer lineRenderer;
    public List<Collider> colliders = new List<Collider>();
    private List<Collider> GetColliders() { return colliders; }

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
}
