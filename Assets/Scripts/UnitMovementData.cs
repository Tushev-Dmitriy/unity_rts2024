using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementData : MonoBehaviour
{
    public bool isGame = false;
    public GameObject boxUI;
    public RectTransform selectionBox;
    public GameObject unitsInTownCenter;
    public List<GameObject> unitsInSelection;
    public bool isOneUnit = false;
    public GameObject unit;
    public GameObject btnBlock;
    public GameObject unitBlock;
}
