using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitDataUI : MonoBehaviour
{
    [Header("Other scripts")]
    public UnitCanvasController unitCanvasController;

    [Header("Units icons")]
    public Sprite[] unitsIcons;
    public Sprite[] builderIcons;
    public Sprite[] unitActionsIcons;

    [Header("Units SO")]
    public List<ScriptableObject> unitsList = new List<ScriptableObject>();

    public void SetUnits(List<ScriptableObject> units)
    {
        unitsList = units;
        AddNewInfoForUnits();
    }

    private void AddNewInfoForUnits()
    {
        foreach (ScriptableObject unit in unitsList)
        {
            if (unit is AttackUnit attackUnit)
            {
                switch (attackUnit.unitName)
                {
                    case "Archer":
                        attackUnit.icon = unitsIcons[0];
                        break;
                    case "Catapult":
                        attackUnit.icon = unitsIcons[2];
                        break;
                    case "HeavyWarrior":
                        attackUnit.icon = unitsIcons[4];
                        break;
                    case "Spearman":
                        attackUnit.icon = unitsIcons[6];
                        break;
                }
            }
            else if (unit is Builder builderUnit)
            {
                builderUnit.icon = unitsIcons[1];
            }
            else if (unit is Healer healerUnit)
            {
                healerUnit.icon = unitsIcons[3];
            }
            else if (unit is SiegeTower siegeTowerUnit)
            {
                siegeTowerUnit.icon = unitsIcons[5];
            }
        }
    }

    private Object GetUnitInfoBySO(int unitType)
    {
        foreach (ScriptableObject unit in unitsList)
        {
            if (unit is AttackUnit attackUnit)
            {
                if (attackUnit.unitName == "Archer" && unitType == 1)
                {
                    return attackUnit;
                } else if (attackUnit.unitName == "Catapult" && unitType == 2)
                {
                    return attackUnit;
                } else if (attackUnit.unitName == "HeavyWarrior" && unitType == 3)
                {
                    return attackUnit;
                } else if (attackUnit.unitName == "Spearman" && unitType == 4)
                {
                    return attackUnit;
                }
            }
            else if (unit is Builder builderUnit && unitType == 5)
            {
                return builderUnit;
            }
            else if (unit is Healer healerUnit && unitType == 6)
            {
                return healerUnit;
            }
            else if (unit is SiegeTower siegeTowerUnit && unitType == 7)
            {
                return siegeTowerUnit;
            }
        }

        return null;
    }

    public void AddInfoOnUnit(GameObject unit)
    {
        string nameOfUnit = unit.name;
        UnitDataController unitData;
        unitData = unit.GetOrAddComponent<UnitDataController>();
        unitData.unitCanvasController = unitCanvasController;
        
        if (unit.GetComponent<BuilderResource>() != null) 
        {
            unit.GetComponent<BuilderResource>().unitCanvasController = unitCanvasController;
        } else if(unit.GetComponent<AttackUnitResource>() != null)
        {
            unit.GetComponent<AttackUnitResource>().unitCanvasController = unitCanvasController;
        }

        switch (nameOfUnit)
        {
            case "Archer(Clone)":
                AttackUnit archerUnit = GetUnitInfoBySO(1) as AttackUnit;
                unitData.unitType = Type.AttackUnit;
                unitData.unitIcon = archerUnit.icon;
                unitData.unitName = archerUnit.unitName;
                unitData.unitHealh = archerUnit.health;
                unitData.unitMaxHealth = archerUnit.maxHealth;
                unitData.movementSpeed = archerUnit.movementSpeed;
                unitData.detectionRadius = archerUnit.detectionRadius;
                unitData.minRange = archerUnit.minRange;
                unitData.maxRange = archerUnit.maxRange;
                unitData.attackDelay = archerUnit.attackDelay;
                unitData.damage = archerUnit.damage;
                break;
            case "Catapult(Clone)":
                AttackUnit catapultUnit = GetUnitInfoBySO(2) as AttackUnit;
                unitData.unitType = Type.AttackUnit;
                unitData.unitIcon = catapultUnit.icon;
                unitData.unitName = catapultUnit.unitName;
                unitData.unitHealh = catapultUnit.health;
                unitData.unitMaxHealth = catapultUnit.maxHealth;
                unitData.movementSpeed = catapultUnit.movementSpeed;
                unitData.detectionRadius = catapultUnit.detectionRadius;
                unitData.minRange = catapultUnit.minRange;
                unitData.maxRange = catapultUnit.maxRange;
                unitData.attackDelay = catapultUnit.attackDelay;
                unitData.damage = catapultUnit.damage;
                break;
            case "Heavy Warrior(Clone)":
                AttackUnit warroirUnit = GetUnitInfoBySO(3) as AttackUnit;
                unitData.unitType = Type.AttackUnit;
                unitData.unitIcon = warroirUnit.icon;
                unitData.unitName = warroirUnit.unitName;
                unitData.unitHealh = warroirUnit.health;
                unitData.unitMaxHealth = warroirUnit.maxHealth;
                unitData.movementSpeed = warroirUnit.movementSpeed;
                unitData.detectionRadius = warroirUnit.detectionRadius;
                unitData.minRange = warroirUnit.minRange;
                unitData.maxRange = warroirUnit.maxRange;
                unitData.attackDelay = warroirUnit.attackDelay;
                unitData.damage = warroirUnit.damage;
                break;
            case "Knight(Clone)":
                AttackUnit spearmanUnit = GetUnitInfoBySO(4) as AttackUnit;
                unitData.unitType = Type.AttackUnit;
                unitData.unitIcon = spearmanUnit.icon;
                unitData.unitName = spearmanUnit.unitName;
                unitData.unitHealh = spearmanUnit.health;
                unitData.unitMaxHealth = spearmanUnit.maxHealth;
                unitData.movementSpeed = spearmanUnit.movementSpeed;
                unitData.detectionRadius = spearmanUnit.detectionRadius;
                unitData.minRange = spearmanUnit.minRange;
                unitData.maxRange = spearmanUnit.maxRange;
                unitData.attackDelay = spearmanUnit.attackDelay;
                unitData.damage = spearmanUnit.damage;
                break;
            case "Villager(Clone)":
                Builder builderUnit = GetUnitInfoBySO(5) as Builder;
                unitData.unitType = Type.Builder;
                unitData.unitIcon = builderUnit.icon;
                unitData.unitName = builderUnit.unitName;
                unitData.unitHealh = builderUnit.health;
                unitData.unitMaxHealth = builderUnit.maxHealth;
                unitData.movementSpeed = builderUnit.movementSpeed;
                unitData.detectionRadius = builderUnit.detectionRadius;
                unitData.resourceGatheringSpeed = builderUnit.resourceGatheringSpeed;
                unitData.repairSpeed = builderUnit.repairSpeed;
                break;
            case "Priest(Clone)":
                Healer healerUnit = GetUnitInfoBySO(6) as Healer;
                unitData.unitType = Type.Healer;
                unitData.unitIcon = healerUnit.icon;
                unitData.unitName = healerUnit.unitName;
                unitData.unitHealh = healerUnit.health;
                unitData.unitMaxHealth = healerUnit.maxHealth;
                unitData.movementSpeed = healerUnit.movementSpeed;
                unitData.detectionRadius = healerUnit.detectionRadius;
                unitData.minDist = healerUnit.minDist;
                unitData.maxDist = healerUnit.maxDist;
                unitData.healDelay = healerUnit.healDelay;
                unitData.heal = healerUnit.heal;
                break;
            case "Archer Tower(Clone)":
                SiegeTower towerUnit = GetUnitInfoBySO(7) as SiegeTower;
                unitData.unitType = Type.SiegeTower;
                unitData.unitIcon = towerUnit.icon;
                unitData.unitName = towerUnit.unitName;
                unitData.unitHealh = towerUnit.health;
                unitData.unitMaxHealth = towerUnit.maxHealth;
                unitData.movementSpeed = towerUnit.movementSpeed;
                unitData.detectionRadius = towerUnit.detectionRadius;
                unitData.capacity = towerUnit.capacity;
                break;
        }
    }
}
