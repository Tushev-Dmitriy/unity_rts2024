using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceCost
{
    public string resourceName;
    public int amount;
}

public enum Type
{
    AttackUnit,
    Builder,
    Healer,
    SiegeTower,
    Entity
}

public class Unit
{
    public Type unitType;

    public string unitName;
    public float movementSpeed;
    public float health;
    public List<ResourceCost> trainingCost;
    public float detectionRadius;
    public Sprite icon;

    public float minRange;
    public float maxRange;
    public float attackDelay;
    public float damage;

    public float resourceGatheringSpeed;
    public float repairSpeed;
    public float repairEfficiency;

    public float minDist;
    public float maxDist;
    public float healDelay;
    public float heal;

    public int capacity;
}
