using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

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
    public Dictionary<string, int> constructionCost;
    public List<string> productOfBuilding;
    public Sprite icon;

    public float buildingZoneRadius;

    public bool resourceCapacityIncrease;

    public float detectionRadius;
    public int archerCapacity;
    public Dictionary<string, int> attackRange;
    public float attackDelay;
    public int damagePerArcher;

}
