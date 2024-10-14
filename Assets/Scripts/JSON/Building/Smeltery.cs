using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Smeltery : ScriptableObject
{
    public int strength;
    public Dictionary<string, int> constructionCost;
    public List<string> productOfBuilding;
    public Sprite icon;
}
