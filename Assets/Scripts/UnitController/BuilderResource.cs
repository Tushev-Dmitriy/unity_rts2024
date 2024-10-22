using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuilderResource : MonoBehaviour
{
    [Header("Other scripts")]
    public UnitCanvasController unitCanvasController;

    [Header("Resource data")]
    public bool isGetResource = false;
    public bool isGoToTownHall = false;
    public bool isMoving = false;
    public bool isBuilding = false;

    public void UpdateBuilderResourcesCanvas()
    {
        unitCanvasController.BuilderResourcesSetup(gameObject, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CubeForResources"))
        {
            BuildingDataController townHallData = other.gameObject.transform.parent.GetComponent<BuildingDataController>();
            UnitDataController unitData = gameObject.GetComponent<UnitDataController>();

            Dictionary<string, int> itemDictionary = new Dictionary<string, int>();

            if (townHallData.items != null)
            {
                foreach (var buildingItem in townHallData.items)
                {
                    itemDictionary[buildingItem.itemName] = buildingItem.amount;
                }
            }

            foreach (UnitItems item in unitData.items)
            {
                if (itemDictionary.ContainsKey(item.itemName))
                {
                    itemDictionary[item.itemName] += item.amount;
                }
                else
                {
                    itemDictionary[item.itemName] = item.amount;
                }
            }

            townHallData.items = itemDictionary.Select(kvp => new BuildingItems(kvp.Key, kvp.Value)).ToList();

            unitData.items.Clear();
            UpdateBuilderResourcesCanvas();
        }
    }
}
