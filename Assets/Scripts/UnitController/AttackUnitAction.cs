using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AttackUnitAction : MonoBehaviour
{
    public List<GameObject> attackUnitActionBtns;

    public void SetAttackUnitActionBtns(List<Image> attackUnitActionImg, GameObject unit)
    {
        attackUnitActionBtns.Clear();

        for (int i = 0; i < attackUnitActionImg.Count; i++)
        {
            attackUnitActionBtns.Add(attackUnitActionImg[i].gameObject.transform.parent.gameObject);
        }

        attackUnitActionBtns[0].GetComponent<Button>().onClick.AddListener(delegate { SetupMove(unit); });
        attackUnitActionBtns[1].GetComponent<Button>().onClick.AddListener(delegate { SetupAttack(unit); });
        attackUnitActionBtns[2].GetComponent<Button>().onClick.AddListener(delegate { SetupPatrolling(unit); });
    }

    public void SetupActions(int numOfAction, GameObject unit)
    {
        switch (numOfAction) 
        {
            case 0:
                SetupMove(unit);
                break;
            case 1:
                SetupAttack(unit);
                break;
            case 2:
                SetupPatrolling(unit);
                break;
        }
    }
    private void SetupMove(GameObject unit)
    {
        AttackUnitResource attackUnitResource = unit.GetComponent<AttackUnitResource>();
        bool currentFlag = attackUnitResource.isMoving;

        ClearAllFlags(unit);
        attackUnitResource.isMoving = !currentFlag;
        attackUnitResource.SetAgent(unit, GetComponent<AttackUnitAction>());
    }

    private void SetupAttack(GameObject unit)
    {
        AttackUnitResource attackUnitResource = unit.GetComponent<AttackUnitResource>();
        bool currentFlag = attackUnitResource.isAttacking;

        ClearAllFlags(unit);
        attackUnitResource.isAttacking = !currentFlag;
        attackUnitResource.SetAgent(unit, GetComponent<AttackUnitAction>());
    }

    private void SetupPatrolling(GameObject unit)
    {
        AttackUnitResource attackUnitResource = unit.GetComponent<AttackUnitResource>();
        bool currentFlag = attackUnitResource.isPatrolling;

        ClearAllFlags(unit);
        attackUnitResource.isPatrolling = !currentFlag;
        attackUnitResource.SetAgent(unit, GetComponent<AttackUnitAction>());
    }

    private void ClearAllFlags(GameObject unit)
    {
        AttackUnitResource attackUnitResource = unit.GetComponent<AttackUnitResource>();
        attackUnitResource.isMoving = false;
        attackUnitResource.isAttacking = false;
        attackUnitResource.isPatrolling = false;
    }
}
