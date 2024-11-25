using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionsNewBuildings : MonoBehaviour
{
    [Header("Other scripts")]
    public UnitController unitController;
    public BuildingDataUI buildingDataUI;
    public InfoPanelController infoPanelController;

    [Header("User base obj")]
    public GameObject userBase;
    public GameObject[] buildingsObj;
    public Material[] lineMaterials;

    private bool canBuild = false;

    public void ConstructionBuildings(int numOfBuild)
    {
        GameObject tempBuilding = Instantiate(buildingsObj[numOfBuild], userBase.transform);
        tempBuilding.tag = "NewUserBuilding";
        buildingDataUI.AddInfoOnBuild(tempBuilding);
        List<ConstructionCost> tempCost = tempBuilding.GetComponent<BuildingDataController>().costList;
        List<BuildingItems> tempItems = userBase.transform.GetChild(0).GetComponent<BuildingDataController>().items;

        if (tempItems ==  null )
        {
            canBuild = false;
        } else
        {
            canBuild = CheckBuildingRequirements(tempCost, tempItems);
        }

        if (!canBuild)
        {
            Destroy(tempBuilding);
        } else
        {
            Vector3 buildingScale = Vector3.zero;
            Vector3 buildingPos = Vector3.zero;

            switch (numOfBuild)
            {
                case 0:
                    buildingScale = new Vector3(0.02f, 10, 0.02f);
                    buildingPos = new Vector3(0, 0.113f, 0);
                    break;
                case 1:
                    buildingScale = new Vector3(0.024f, 12f, 0.024f);
                    break;
                case 2:
                    buildingScale = new Vector3(0.03f, 15, 0.03f);
                    break;
                case 3:
                    buildingScale = new Vector3(0.0023f, 1.15f, 0.0023f);
                    break;
                case 4:
                    buildingScale = new Vector3(0.03f, 15, 0.03f);
                    break;
                case 5:
                    buildingScale = new Vector3(0.03f, 15, 0.03f);
                    break;
                case 6:
                    buildingScale = new Vector3(0.0017f, 0.88f, 0.0017f);
                    break;
                case 7:
                    buildingScale = new Vector3(0.0023f, 1.15f, 0.0023f);
                    break;
                case 8:
                    buildingScale = new Vector3(0.0023f, 1.15f, 0.0023f);
                    break;
            }

            tempBuilding.transform.position = buildingPos;
            tempBuilding.transform.localScale = buildingScale;
            tempBuilding.AddComponent<NewBuildingController>();
            tempBuilding.GetComponent<NewBuildingController>().lineMaterials = lineMaterials;
            tempBuilding.GetComponent<NewBuildingController>().userBase = userBase.transform.GetChild(0).gameObject;
            unitController.isBuild = true;
            unitController.tempUnit = tempBuilding;
            canBuild = false;
        }
    }

    private bool CheckBuildingRequirements(List<ConstructionCost> tempCost, List<BuildingItems> tempItems)
    {
        infoPanelController.CheckItemInTownHall();
        foreach (ConstructionCost cost in tempCost)
        {
            BuildingItems item = tempItems.Find(i => i.itemName == cost.resourceName);
            if (item != null)
            {
                if (item.amount >= cost.amount)
                {
                    if (item.itemName == "")

                    item.amount -= cost.amount;
                    if (item.amount == 0)
                    {
                        tempItems.Remove(item);
                    }
                    return true;
                }
            }
        }
        return false;
    }
}
