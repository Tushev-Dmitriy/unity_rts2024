using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mill : ScriptableObject
{
    public int strength;
    public List<ConstructionCost> constructionCost;
    public List<BuildingItems> items;
    public List<string> productOfBuilding;
    public Sprite icon;
}
