using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Watchtower : ScriptableObject
{
    public int strength;
    public List<ConstructionCost> constructionCost;
    public List<string> productOfBuilding;
    public Sprite icon;

    public float detectionRadius;
    public float buildingZoneRadius;
    public int archerCapacity;
    public List<int> attackRange;
    public float attackDelay;
    public int damagePerArcher;
}
