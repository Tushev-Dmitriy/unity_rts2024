using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDataController : MonoBehaviour
{
    public BuildingCanvasController buildingCanvasController;

    public BuildingType buildingType;
    public Sprite buildingIcon;
    public string buildingName;
    public int buildingMaxHealth;
    public int buildingHealh;

    public void SetBuildingInfoInUI()
    {
        buildingCanvasController.SetupInfo(buildingType, buildingIcon, buildingName, 
            buildingMaxHealth, buildingHealh);
    }
}
