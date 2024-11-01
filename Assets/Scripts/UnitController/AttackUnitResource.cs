using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUnitResource : MonoBehaviour
{
    [Header("Other scripts")]
    public UnitCanvasController unitCanvasController;

    [Header("Resource data")]
    public bool isMoving = false;
    public bool isAttacking = false;
    public bool isPatrolling = false;
}
