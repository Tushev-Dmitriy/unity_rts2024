using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private bool isMove = false;
    private GameObject tempUnit;
    private Vector3 mousePressPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Unit"))
                {
                    tempUnit = hit.collider.gameObject;
                    mousePressPosition = hit.point;
                    isMove = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(1) && isMove)
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 20))
            {
                Vector3 mouseReleasePosition = hit.point;
                Vector3 moveDirection = mouseReleasePosition - mousePressPosition;
                if (tempUnit.CompareTag("Unit"))
                {
                    isMove = false;
                    tempUnit.transform.position = Vector3.MoveTowards(mousePressPosition, moveDirection, 0);
                }

                tempUnit = null;
            }
        }
    }
}
