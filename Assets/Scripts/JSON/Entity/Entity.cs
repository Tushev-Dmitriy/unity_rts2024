using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceCost
{
    public string resourceName;
    public int amount;
}

public class Entity : MonoBehaviour
{
    public string unitName;
    public float movementSpeed;
    public int health;
    public int maxHealth;
    public List<ResourceCost> trainingCost;
    public float detectionRadius;
    public Sprite icon;
}
