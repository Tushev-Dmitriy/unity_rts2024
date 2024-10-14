using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Workshop : ScriptableObject
{
    public int strength;
    public Dictionary<string, int> constructionCost;
    public List<string> productOfBuilding;
    public Sprite icon;
}
