using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDataController : MonoBehaviour
{
    public BuildingCanvasController buildingCanvasController;

    public BuildingType buildingType;
    public Sprite buildingIcon;
    public string buildingName;
    public List<ConstructionCost> costList;
    public int buildingMaxHealth;
    public int buildingHealh;
    public List<BuildingItems> items;

    public void SetBuildingInfoInUI(bool isBuilding)
    {
        buildingCanvasController.SetupInfo(buildingType, buildingIcon, buildingName, 
            buildingMaxHealth, buildingHealh, isBuilding);
    }

    public void SetupBuildingResource(GameObject building)
    {
        if (buildingType == BuildingType.TownHall)
        {
            buildingCanvasController.SetupResourceData(building);
        }
    }
}
