using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Watchtower : ScriptableObject
{
    public int strength;
    public Dictionary<string, int> constructionCost;
    public List<string> productOfBuilding;
    public Sprite icon;

    public float detectionRadius;
    public float buildingZoneRadius;
    public int archerCapacity;
    public Dictionary<string, int> attackRange;
    public float attackDelay;
    public int damagePerArcher;
}
