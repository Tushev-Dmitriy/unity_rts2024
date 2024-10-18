using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderResource : MonoBehaviour
{
    [Header("Other scripts")]
    public UnitCanvasController unitCanvasController;

    [Header("Resource data")]
    public List<int> resourcesCount = new List<int>();
    public bool isGetResource = false;
    public bool isGoToTownHall = false;
    public bool isMoving = false;
    public bool isBuilding = false;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            resourcesCount.Add(0);
        }
    }

    public void IncreaseResource(int num)
    {
        resourcesCount[num]++;
        unitCanvasController.IncreaseResource(num, resourcesCount[num]);
    }
}
