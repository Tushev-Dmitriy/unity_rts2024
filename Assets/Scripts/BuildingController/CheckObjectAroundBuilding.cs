using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObjectAroundBuilding : MonoBehaviour
{
    public bool isCorrect = false;
    public Material lineRendererMaterial;

    private List<GameObject> objectsInCollider = new List<GameObject>();

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale, Quaternion.identity);
        
        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.tag == "MapObject")
            {
                objectsInCollider.Add(collider.gameObject);
            }
        }

        if (objectsInCollider.Count == 0)
        {
            Debug.Log(1);
        } else
        {
            Debug.Log(2);
        }
    }
}
