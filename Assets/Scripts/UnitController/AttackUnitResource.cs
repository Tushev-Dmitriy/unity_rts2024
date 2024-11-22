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

    private UnitDataController userUnit;
    private GameObject enemyUnit;
    private bool goToEnemy = false;
    private bool onFight = false;

    public void SetAgent(GameObject unit)
    {
        userUnit = unit.GetComponent<UnitDataController>();
        agent = unit.GetComponent<NavMeshAgent>();
        startUnitPos = unit.transform.position;
        agent.speed = userUnit.movementSpeed;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (agent != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.CompareTag("Plane") && isPatrolling)
                    {
                        targetPoint = hit.point;
                        movingToTarget = true;
                        agent.SetDestination(targetPoint);
                    }
                    else if (hit.collider.CompareTag("EnemyUnit") && isAttacking)
                    {
                        enemyUnit = hit.collider.gameObject;
                        goToEnemy = true;
                    }
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

        if (goToEnemy)
        {
            PathToEnemy();
        }

        if (enemyUnit != null && isAttacking && !onFight)
        {
            float distanceToTarget = Vector3.Distance(agent.transform.position, enemyUnit.transform.position);
            if (distanceToTarget <= userUnit.maxRange)
            {
                StartAttack();
            }
        }
    }

    private void PathToEnemy()
    {
        goToEnemy = false;
        agent.SetDestination(enemyUnit.transform.position);
    }

    private void StartAttack()
    {
        agent.ResetPath();
        onFight = true;
        UnitDataController enemyUnitController = enemyUnit.GetComponent<UnitDataController>();
        ParticleSystem enemyUnitParticles = enemyUnit.GetComponent<ParticleSystem>();
        StartCoroutine(userUnitAttack(userUnit.attackDelay, userUnit.damage, enemyUnitController, enemyUnitParticles));
    }

    private IEnumerator userUnitAttack(float delay, float damage, UnitDataController enemyUnitController, ParticleSystem enemyUnitParticles)
    {
        if (!isAttacking)
        {
            yield break;
        }

        if (enemyUnitController.unitHealh - damage >= 0)
        {
            enemyUnitController.unitHealh -= damage;
            enemyUnitParticles.Play();
            SetupNewUnitInfo(enemyUnitController);
            yield return new WaitForSeconds(delay);
            StartCoroutine(userUnitAttack(delay, damage, enemyUnitController, enemyUnitParticles));
        }
        else
        {
            Destroy(enemyUnitController.gameObject);
            onFight = false;
            yield break;
        }
    }

    private void SetupNewUnitInfo(UnitDataController userUnit)
    {
        userUnit.SetUnitsInfoInUI();
        userUnit.SetBuilderResource(userUnit.gameObject);
    }
}
