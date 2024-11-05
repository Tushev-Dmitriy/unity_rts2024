using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AttackUnitResource : MonoBehaviour
{
    [Header("Other scripts")]
    public UnitCanvasController unitCanvasController;

    [Header("Resource data")]
    public bool isMoving = false;
    public bool isAttacking = false;
    public bool isPatrolling = false;

    private NavMeshAgent agent;
    private NavMeshPath navMeshPath;
    private float range;
    public Transform centerPoint;

    void Start()
    {
        range = 10f;
        navMeshPath = new NavMeshPath();
    }

    public void SetAgent()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isPatrolling)
        {
            if (agent.remainingDistance <= agent.stoppingDistance + 0.5f)
            {
                Vector3 point;
                centerPoint.position = new Vector3(centerPoint.position.x, 0.05f, centerPoint.position.z);
                if (RandomPoint(centerPoint.position, range, out point))
                {
                    agent.SetDestination(point);
                }
            }
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas) && CanReachArea(randomPoint))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    private bool CanReachArea(Vector3 target)
    {
        return agent.CalculatePath(target, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete;
    }

}
