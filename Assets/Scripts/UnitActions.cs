using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class UnitActions : MonoBehaviour
{
    public bool isMoving;
    public bool isAttacking;
    public bool isPatrolling;
    public bool isHealing;
    public bool isRepairing;
    public bool isGathering;
    public bool isUpgrading;

    public void MoveUnit(GameObject tempUnit)
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 target = new Vector3(hit.point.x, tempUnit.transform.position.y, hit.point.z);
            }
        }
    }

    public void AttackUnit(GameObject tempUnit)
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

            }
        }
    }
}
