using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Builder : ScriptableObject
{
    public string unitName;
    public float movementSpeed;
    public float health;
    public float maxHealth;
    public List<ResourceCost> trainingCost;
    public float detectionRadius;
    public Sprite icon;

    public float resourceGatheringSpeed;
    public float repairSpeed;
    public float repairEfficiency;
}
