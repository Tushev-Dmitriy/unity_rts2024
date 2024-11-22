using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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
    private bool isFight = false;

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
            if (!isFight)
            {
                isDefaultAction = false;

                distanceToTarget = Vector3.Distance(agent.transform.position, userUnit.transform.position);
                if (distanceToTarget <= unitDataController.detectionRadius)
                {
                    agent.SetDestination(userUnit.transform.position);
                    isFight = true;
                    StartEnemyUnitAttack();
                }

                if (distanceToTarget <= unitDataController.minRange)
                {
                    agent.SetDestination(startUnitPos);
                    isFight = true;
                    StartEnemyUnitAttack();
                }
            } else
            {
                distanceToTarget = Vector3.Distance(agent.transform.position, userUnit.transform.position);
                if (unitDataController.detectionRadius <= distanceToTarget)
                {
                    userUnit = null;
                    agent.SetDestination(startUnitPos);
                    isFight = false;
                }
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

    private void StartEnemyUnitAttack()
    {
        float delay = unitDataController.attackDelay;
        float damage = unitDataController.damage;
        UnitDataController userUnitController = userUnit.GetComponent<UnitDataController>();
        ParticleSystem userParticleSystem = userUnit.GetComponent<ParticleSystem>();
        StartCoroutine(EnemyUnitAttack(delay, damage, userUnitController, userParticleSystem));
    }

    private IEnumerator EnemyUnitAttack(float delay, float damage, UnitDataController userUnitController, ParticleSystem userUnitParticles)
    {
        if (!isFight)
        {
            yield break;
        }

        if (userUnitController.unitHealh - damage >= 0)
        {
            userUnitController.unitHealh -= damage;
            userUnitParticles.Play();
            SetupNewUnitInfo(userUnitController);
            yield return new WaitForSeconds(delay);
            StartCoroutine(EnemyUnitAttack(delay, damage, userUnitController, userUnitParticles));
        }
        else
        {
            Destroy(userUnitController.gameObject);
            isFight = false;
            yield break;
        }
    }

    private void SetupNewUnitInfo(UnitDataController userUnit)
    {
        userUnit.SetUnitsInfoInUI();
        userUnit.SetBuilderResource(userUnit.gameObject);
    }
}
