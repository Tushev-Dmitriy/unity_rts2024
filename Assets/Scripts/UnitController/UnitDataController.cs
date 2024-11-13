using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataController : MonoBehaviour
{
    public UnitCanvasController unitCanvasController;

    [Header("Main info")]
    public Type unitType;
    public Sprite unitIcon;
    public string unitName;
    public float unitMaxHealth;
    public float unitHealh;
    public List<UnitItems> items;

    [Header("Extra info")]
    public float movementSpeed;
    public float detectionRadius;

    [Header("Attack unit")]
    public float minRange;
    public float maxRange;
    public float attackDelay;
    public float damage;

    [Header("Builder unit")]
    public float resourceGatheringSpeed;
    public float repairSpeed;
    public float repairEfficiency;

    [Header("Healer unit")]
    public float minDist;
    public float maxDist;
    public float healDelay;
    public float heal;

    [Header("Siege tower")]
    public int capacity;

    public void SetUnitsInfoInUI()
    {
        unitCanvasController.SetupInfo(unitType, unitIcon, unitName, unitMaxHealth, unitHealh);
    }

    public void SetBuilderResource(GameObject unit)
    {
        if (unitType == Type.Builder)
        {
            unitCanvasController.BuilderResourcesSetup(unit, true);
        } else if (unitType == Type.AttackUnit)
        {
            unitCanvasController.AttackUnitResourcesSetup(unit);
        }
    }
}
