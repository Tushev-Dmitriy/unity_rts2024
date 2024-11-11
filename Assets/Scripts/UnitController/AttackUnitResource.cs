using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class AttackUnitResource : MonoBehaviour
{
    [Header("Other scripts")]
    public UnitCanvasController unitCanvasController;

    [Header("Resource data")]
    public bool isMoving = false;
    public bool isAttacking = false;
    public bool isPatrolling = false;

    private NavMeshAgent agent;
    private Vector3 startUnitPos;
    private Vector3 targetPoint = Vector3.zero;
    private bool movingToTarget = true;

    private float targetRadius = 0.5f;

    public void SetAgent(GameObject unit)
    {
        agent = unit.GetComponent<NavMeshAgent>();
        startUnitPos = unit.transform.position;
    }

    void Update()
    {
        if (agent != null)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (Input.GetMouseButtonDown(0) && isPatrolling)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.CompareTag("Plane"))
                    {
                        targetPoint = hit.point;
                        movingToTarget = true;
                        agent.SetDestination(targetPoint);
                    }
                }
            }

            if (isPatrolling && targetPoint != Vector3.zero)
            {
                float distanceToTarget = Vector3.Distance(agent.transform.position, movingToTarget ? targetPoint : startUnitPos);
                if (distanceToTarget <= targetRadius)
                {
                    movingToTarget = !movingToTarget;
                    agent.SetDestination(movingToTarget ? targetPoint : startUnitPos);
                }
            }
        }
    }
}
