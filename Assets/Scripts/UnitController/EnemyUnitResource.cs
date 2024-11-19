using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyUnitResource : MonoBehaviour
{
    private UnitDataController unitDataController;
    private NavMeshAgent agent;
    private Vector3 startUnitPos;

    private GameObject userUnit;
    private float distanceToTarget = 0f;
    private float detectionRadius = 0f;
    private bool movingToTarget = false;
    private Vector3 targetPoint = Vector3.zero;
    private bool isDefaultAction = false;
    private Vector3 point = Vector3.zero;
    private NavMeshPath navMeshPath;

    public void SetEnemyUnitInfo()
    {
        unitDataController = GetComponent<UnitDataController>();
        agent = GetComponent<NavMeshAgent>();
        startUnitPos = transform.position;
        agent.speed = unitDataController.movementSpeed;
        detectionRadius = unitDataController.detectionRadius;
        navMeshPath = new NavMeshPath();
}

    private void Update()
    {
        CheckUserUnitInArea();
        SetAttackOnUserUnit();
        DefaultUnitAction();
    }

    private void CheckUserUnitInArea()
    {
        Collider[] hitColliders = Physics.OverlapSphere(startUnitPos, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "UnitToMove")
            {
                userUnit = hitCollider.gameObject;
            }
        }
    }

    private void SetAttackOnUserUnit()
    {
        if (userUnit != null)
        {
            isDefaultAction = false;

            distanceToTarget = Vector3.Distance(agent.transform.position, userUnit.transform.position);
            if (distanceToTarget <= unitDataController.detectionRadius && )
            {
                agent.SetDestination(userUnit.transform.position);
            }
            else
            {
                userUnit = null;
                agent.SetDestination(startUnitPos);
            }
        }
    }

    private void DefaultUnitAction()
    {
        if (Vector3.Distance(agent.transform.position, startUnitPos) <= 1.5f && !isDefaultAction)
        {
            if (RandomPoint(transform.position, 4f, out point))
            {
                isDefaultAction = true;
                targetPoint = point;
            }
        }

        if (isDefaultAction)
        {
            float distanceToTarget = Vector3.Distance(agent.transform.position, movingToTarget ? targetPoint : startUnitPos);
            if (distanceToTarget <= detectionRadius)
            {
                movingToTarget = !movingToTarget;
                agent.SetDestination(movingToTarget ? targetPoint : startUnitPos);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            if (CanReachArea(result))
            {
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

    public bool CanReachArea(Vector3 target)
    {
        return agent.CalculatePath(target, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete;
    }
}
