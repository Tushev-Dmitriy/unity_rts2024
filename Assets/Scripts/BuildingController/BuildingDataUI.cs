using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDataUI : MonoBehaviour
{
    [Header("Other scripts")]
    public BuildingCanvasController buildingCanvasController;

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
                Barracks.icon = buildingsIcons[0];
            } else if (building is House House)
            {
                House.icon = buildingsIcons[1];
            } else if (building is Workshop Workshop)
            {
                Workshop.icon = buildingsIcons[2];
            } else if (building is Temple Temple)
            {
                Temple.icon = buildingsIcons[3];
            } else if (building is Fields Fields)
            {
                Fields.icon = buildingsIcons[4];
            } else if (building is Mill Mill)
            {
                Mill.icon = buildingsIcons[5];
            } else if (building is Watchtower Watchtower)
            {
                Watchtower.icon = buildingsIcons[6];
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
                return barracks;
            }
            else if (building is Workshop workshop && buildingType == 2)
            {
                return workshop;
            }
            else if (building is Temple temple && buildingType == 3)
            {
                return temple;
            }
            else if (building is Fields fields && buildingType == 4)
            {
                return fields;
            }
            else if (building is Mill mill && buildingType == 5)
            {
                return mill;
            }
            else if (building is Watchtower watchtower && buildingType == 6)
            {
                return watchtower;
            }
            else if (building is Warehouse warehouse && buildingType == 7)
            {
                return warehouse;
            }
            else if (building is TownHall townHall && buildingType == 8)
            {
                return townHall;
            }
            else if (building is Smeltery smeltery && buildingType == 9)
            {
                return smeltery;
            }
        }

        return null;
    }

    public void AddInfoOnBuild(GameObject building)
    {
        string nameOfBuilding = building.name;
        BuildingDataController buildingData;

        if (building.GetComponent<BuildingDataController>() == null)
        {
            buildingData = building.AddComponent<BuildingDataController>();
        }
        else
        {
            buildingData = building.GetComponent<BuildingDataController>();
        }

        buildingData.buildingCanvasController = buildingCanvasController;

        switch (nameOfBuilding)
        {
            case "Barraks(Clone)":
                Barracks barracksBuilding = GetBuildingInfoBySO(1) as Barracks;
                buildingData.buildingType = BuildingType.Barracks;
                buildingData.buildingIcon = barracksBuilding.icon;
                buildingData.buildingName = BuildingType.Barracks.ToString();
                buildingData.buildingMaxHealth = barracksBuilding.strength;
                buildingData.buildingHealh = barracksBuilding.strength;
                break;
            case "Workshop(Clone)":
                Workshop workshopBuilding = GetBuildingInfoBySO(2) as Workshop;
                buildingData.buildingType = BuildingType.Workshop;
                buildingData.buildingIcon = workshopBuilding.icon;
                buildingData.buildingName = BuildingType.Workshop.ToString();
                buildingData.buildingMaxHealth = workshopBuilding.strength;
                buildingData.buildingHealh = workshopBuilding.strength;
                break;
            case "Temple(Clone)":
                Temple templeBuilding = GetBuildingInfoBySO(3) as Temple;
                buildingData.buildingType = BuildingType.Temple;
                buildingData.buildingIcon = templeBuilding.icon;
                buildingData.buildingName = BuildingType.Temple.ToString();
                buildingData.buildingMaxHealth = templeBuilding.strength;
                buildingData.buildingHealh = templeBuilding.strength;
                break;
            case "Fields(Clone)":
                Fields fieldsBuilding = GetBuildingInfoBySO(4) as Fields;
                buildingData.buildingType = BuildingType.Fields;
                buildingData.buildingIcon = fieldsBuilding.icon;
                buildingData.buildingName = BuildingType.Fields.ToString();
                buildingData.buildingMaxHealth = fieldsBuilding.strength;
                buildingData.buildingHealh = fieldsBuilding.strength;
                break;
            case "Mill(Clone)":
                Mill millBuilding = GetBuildingInfoBySO(5) as Mill;
                buildingData.buildingType = BuildingType.Mill;
                buildingData.buildingIcon = millBuilding.icon;
                buildingData.buildingName = BuildingType.Mill.ToString();
                buildingData.buildingMaxHealth = millBuilding.strength;
                buildingData.buildingHealh = millBuilding.strength;
                break;
            case "Watchtower(Clone)":
                Watchtower watchtowerBuilding = GetBuildingInfoBySO(6) as Watchtower;
                buildingData.buildingType = BuildingType.Watchtower;
                buildingData.buildingIcon = watchtowerBuilding.icon;
                buildingData.buildingName = BuildingType.Watchtower.ToString();
                buildingData.buildingMaxHealth = watchtowerBuilding.strength;
                buildingData.buildingHealh = watchtowerBuilding.strength;
                break;
            case "Warehouse(Clone)":
                Warehouse warehouseBuilding = GetBuildingInfoBySO(7) as Warehouse;
                buildingData.buildingType = BuildingType.Warehouse;
                buildingData.buildingIcon = warehouseBuilding.icon;
                buildingData.buildingName = BuildingType.Warehouse.ToString();
                buildingData.buildingMaxHealth = warehouseBuilding.strength;
                buildingData.buildingHealh = warehouseBuilding.strength;
                break;
            case "TownHall(Clone)":
                TownHall townHallBuilding = GetBuildingInfoBySO(8) as TownHall;
                buildingData.buildingType = BuildingType.TownHall;
                buildingData.buildingIcon = townHallBuilding.icon;
                buildingData.buildingName = BuildingType.TownHall.ToString();
                buildingData.buildingMaxHealth = townHallBuilding.strength;
                buildingData.buildingHealh = townHallBuilding.strength;
                break;
            case "Smeltery(Clone)":
                Smeltery smelteryBuilding = GetBuildingInfoBySO(9) as Smeltery;
                buildingData.buildingType = BuildingType.Smeltery;
                buildingData.buildingIcon = smelteryBuilding.icon;
                buildingData.buildingName = BuildingType.Smeltery.ToString();
                buildingData.buildingMaxHealth = smelteryBuilding.strength;
                buildingData.buildingHealh = smelteryBuilding.strength;
                break;
        }
    }
}
