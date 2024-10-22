using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingItems
{
    public string itemName;
    public int amount;

    public BuildingItems(string itemName, int amount)
    {
        this.itemName = itemName;
        this.amount = amount;
    }
}

public class ConstructionCost
{
    public string resourceName;
    public int amount;
}

public enum BuildingType
{
    Barracks,
    House,
    Workshop,
    Temple,
    Fields,
    Mill,
    Watchtower,
    Warehouse,
    TownHall,
    Smeltery
}
public class Building
{
    public BuildingType buildingType;
    public int strength;
    public List<ConstructionCost> constructionCost;
    public List<BuildingItems> items;
    public List<string> productOfBuilding;
    public Sprite icon;

    public float buildingZoneRadius;

    public bool resourceCapacityIncrease;

    public float detectionRadius;
    public int archerCapacity;
    public List<int> attackRange;
    public float attackDelay;
    public int damagePerArcher;

}
