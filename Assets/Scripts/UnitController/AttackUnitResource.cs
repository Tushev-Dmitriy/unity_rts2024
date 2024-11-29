using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

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
    private AttackUnitAction unitAction;
    private GameObject enemyUnit;
    private bool goToEnemy = false;
    private bool onFight = false;
    private bool attackByRadius = false;

    public void SetAgent(GameObject unit, AttackUnitAction attackUnitAction)
    {
        userUnit = unit.GetComponent<UnitDataController>();
        agent = unit.GetComponent<NavMeshAgent>();
        unitAction = attackUnitAction;
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

        if (agent != null && !goToEnemy && !isAttacking && !isMoving && !attackByRadius)
        {
            Debug.Log(0);
            CheckEnemyUnitAround();
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
            Debug.Log(1);
            PathToEnemy();
        }

        if (enemyUnit != null && isAttacking && !onFight)
        {
            Debug.Log(2);
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

    private void CheckEnemyUnitAround()
    {
        Collider[] collider = Physics.OverlapSphere(agent.transform.position, userUnit.maxRange);
        foreach (Collider col in collider)
        {
            if (col.tag == "EnemyUnit")
            {
                unitAction.SetupActions(1, gameObject);
                enemyUnit = col.gameObject;
                goToEnemy = true;
                attackByRadius = true;
                break;
            }
        }
    }
}
