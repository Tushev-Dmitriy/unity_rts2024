using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDataUI : MonoBehaviour
{
    [Header("Other scripts")]
    public UnitCanvasController unitCanvasController;

    [Header("Buildings SO")]
    public List<ScriptableObject> buildingsList = new List<ScriptableObject>();

    [Header("Buildings icons")]
    public Sprite[] buildingsIcons;

    public void SetBuildings(List<ScriptableObject> buildings)
    {
        buildingsList = buildings;
        AddNewInfoForBuildings();
    }

    private void AddNewInfoForBuildings()
    {
        foreach (ScriptableObject building in buildingsList)
        {
            if (building is Barracks Barracks)
            {
                Barracks.icon = buildingsIcons[6];
            } else if (building is House House)
            {
                House.icon = buildingsIcons[2];
            } else if (building is Workshop Workshop)
            {
                Workshop.icon = buildingsIcons[7];
            } else if (building is Temple Temple)
            {
                Temple.icon = buildingsIcons[4];
            } else if (building is Fields Fields)
            {
                Fields.icon = buildingsIcons[0];
            } else if (building is Mill Mill)
            {
                Mill.icon = buildingsIcons[1];
            } else if (building is Watchtower Watchtower)
            {
                Watchtower.icon = buildingsIcons[8];
            } else if (building is Warehouse Warehouse)
            {
                Warehouse.icon = buildingsIcons[7];
            } else if (building is TownHall TownHall)
            {
                TownHall.icon = buildingsIcons[8];
            } else if (building is Smeltery Smeltery)
            {
                Smeltery.icon = buildingsIcons[9];
            }
        }
    }

    private Object GetBuildingInfoBySO(int buildingType)
    {
        foreach (ScriptableObject building in buildingsList)
        {
            if (building is Barracks barracks && buildingType == 1)
            {
                return building;
            }
            else if (building is Workshop Workshop && buildingType == 2)
            {
                return building;
            }
            else if (building is Temple Temple && buildingType == 3)
            {
                return building;
            }
            else if (building is Fields Fields && buildingType == 4)
            {
                return building;
            }
            else if (building is Mill Mill && buildingType == 5)
            {
                return building;
            }
            else if (building is Watchtower Watchtower && buildingType == 6)
            {
                return building;
            }
            else if (building is Warehouse Warehouse && buildingType == 7)
            {
                return building;
            }
            else if (building is TownHall TownHall && buildingType == 8)
            {
                return building;
            }
            else if (building is Smeltery Smeltery && buildingType == 9)
            {
                return building;
            }
        }

        return null;
    }

    public void AddInfoOnBuild(GameObject building)
    {
        string nameOfBuilding = building.name;
        BuildingDataController buildingData;

        if (building.GetComponent<UnitDataController>() == null)
        {
            buildingData = building.AddComponent<BuildingDataController>();
        }
        else
        {
            buildingData = building.GetComponent<BuildingDataController>();
        }

        buildingData.buildingCanvasController = buildingCanvasController;

        switch (nameOfUnit)
        {
            case "Archer(Clone)":
                AttackUnit archerUnit = GetUnitInfoBySO(1) as AttackUnit;
                unitData.unitType = Type.AttackUnit;
                unitData.unitIcon = archerUnit.icon;
                unitData.unitName = archerUnit.unitName;
                unitData.unitHealh = archerUnit.health;
                unitData.unitMaxHealth = archerUnit.maxHealth;
                break;
            case "Catapult(Clone)":
                AttackUnit catapultUnit = GetUnitInfoBySO(2) as AttackUnit;
                unitData.unitType = Type.AttackUnit;
                unitData.unitIcon = catapultUnit.icon;
                unitData.unitName = catapultUnit.unitName;
                unitData.unitHealh = catapultUnit.health;
                unitData.unitMaxHealth = catapultUnit.maxHealth;
                break;
            case "Heavy Warrior(Clone)":
                AttackUnit warroirUnit = GetUnitInfoBySO(3) as AttackUnit;
                unitData.unitType = Type.AttackUnit;
                unitData.unitIcon = warroirUnit.icon;
                unitData.unitName = warroirUnit.unitName;
                unitData.unitHealh = warroirUnit.health;
                unitData.unitMaxHealth = warroirUnit.maxHealth;
                break;
            case "Knight(Clone)":
                AttackUnit spearmanUnit = GetUnitInfoBySO(4) as AttackUnit;
                unitData.unitType = Type.AttackUnit;
                unitData.unitIcon = spearmanUnit.icon;
                unitData.unitName = spearmanUnit.unitName;
                unitData.unitHealh = spearmanUnit.health;
                unitData.unitMaxHealth = spearmanUnit.maxHealth;
                break;
            case "Villager(Clone)":
                Builder builderUnit = GetUnitInfoBySO(5) as Builder;
                unitData.unitType = Type.Builder;
                unitData.unitIcon = builderUnit.icon;
                unitData.unitName = builderUnit.unitName;
                unitData.unitHealh = builderUnit.health;
                unitData.unitMaxHealth = builderUnit.maxHealth;
                break;
            case "Priest(Clone)":
                Healer healerUnit = GetUnitInfoBySO(6) as Healer;
                unitData.unitType = Type.Healer;
                unitData.unitIcon = healerUnit.icon;
                unitData.unitName = healerUnit.unitName;
                unitData.unitHealh = healerUnit.health;
                unitData.unitMaxHealth = healerUnit.maxHealth;
                break;
            case "Archer Tower(Clone)":
                SiegeTower towerUnit = GetUnitInfoBySO(7) as SiegeTower;
                unitData.unitType = Type.SiegeTower;
                unitData.unitIcon = towerUnit.icon;
                unitData.unitName = towerUnit.unitName;
                unitData.unitHealh = towerUnit.health;
                unitData.unitMaxHealth = towerUnit.maxHealth;
                break;
        }
    }
}
