using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    public string name;
    public float speed;
    public int health;
    public List<Training> training;
    public int detectionArea;
}

public class Training
{
    public string resource;
    public int quantity;
}
