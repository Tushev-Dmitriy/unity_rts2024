using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Healer : ScriptableObject
{
    public string unitName;
    public float movementSpeed;
    public float health;
    public float maxHealth;
    public List<ResourceCost> trainingCost;
    public float detectionRadius;
    public Sprite icon;

    public float minDist;
    public float maxDist;
    public float healDelay;
    public float heal;
}