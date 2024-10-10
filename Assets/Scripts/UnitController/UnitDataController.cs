using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataController : MonoBehaviour
{
    public List<Object> unitsList = new List<Object>();

    public void SetUnits(List<Object> units)
    {
        unitsList = units;
    }

    //private void AddNewInfoForUnits()
    //{
    //    AttackUnit unit = unitsList[0];
    //}
}
