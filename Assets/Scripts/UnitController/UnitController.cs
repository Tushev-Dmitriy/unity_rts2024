using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private bool isMove = false;
    public GameObject tempUnit;
    private Vector3 target;
    private Rigidbody rb;
    private Vector3 clickPos = Vector3.zero;
    private Vector3 click2Pos = Vector3.zero;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag == "UnitToMove")
            {
                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                    tempUnit = null;
                }
                isMove = false;
                tempUnit = hit.collider.gameObject;
                clickPos = hit.point;
            }
        }

        if (tempUnit != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    target = new Vector3(hit.point.x, tempUnit.transform.position.y, hit.point.z);
                    isMove = true;
                    click2Pos = hit.point;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            Vector3 targetPos = target;
            Vector3 currentPos = tempUnit.transform.position;

            float dist = Vector3.Distance(currentPos, targetPos);

            rb = tempUnit.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            if (dist > 0.05f)
            {
                Vector3 directionOfTravel = target - tempUnit.transform.position;
                directionOfTravel.Normalize();
                rb.MovePosition(currentPos + (directionOfTravel * 5f * Time.deltaTime));
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                isMove = false;
            }
        }
    }
}
