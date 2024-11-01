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
    public List<UnitItems> items;

    public void SetUnitsInfoInUI()
    {
        unitCanvasController.SetupInfo(unitType, unitIcon, unitName, unitMaxHealth, unitHealh);
    }

    public void SetBuilderResource(GameObject unit)
    {
        if (unitType == Type.Builder)
        {
            unitCanvasController.BuilderResourcesSetup(unit, true);
        } else if (unitType == Type.AttackUnit)
        {
            unitCanvasController.AttackUnitResourcesSetup(unit);
        }
    }
}
