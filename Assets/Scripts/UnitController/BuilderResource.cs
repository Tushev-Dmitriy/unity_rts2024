using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderResource : MonoBehaviour
{
    [Header("Other scripts")]
    public UnitCanvasController unitCanvasController;

    [Header("Resource data")]
    public bool isGetResource = false;
    public bool isGoToTownHall = false;
    public bool isMoving = false;
    public bool isBuilding = false;

    public void UpdateBuilderResourcesCanvas()
    {
        unitCanvasController.BuilderResourcesSetup(gameObject, false);
    }
}
