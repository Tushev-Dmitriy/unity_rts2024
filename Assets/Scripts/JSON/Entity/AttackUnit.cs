using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackUnit : ScriptableObject
{
    public string unitName;
    public float movementSpeed;
    public float health;
    public float maxHealth;
    public List<ResourceCost> trainingCost;
    public float detectionRadius;
    public Sprite icon;

    public float minRange;
    public float maxRange;
    public float attackDelay;
    public float damage;
}