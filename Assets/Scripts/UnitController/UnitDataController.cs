using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataController : MonoBehaviour
{
    public UnitCanvasController unitCanvasController;

    public Type unitType;
    public Sprite unitIcon;
    public string unitName;
    public float unitMaxHealth;
    public float unitHealh;

    public void SetUnitsInfoInUI()
    {
        unitCanvasController.SetupInfo(unitType, unitIcon, unitName, unitMaxHealth, unitHealh);
    }

    public void SetResourceInfoInUI(GameObject unit)
    {
        if (unitType == Type.Builder)
        {
            unitCanvasController.BuilderResourcesSetup(unit);
        }
    }
}
