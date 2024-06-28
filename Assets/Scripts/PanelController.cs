using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public UnitMovementData unitMovementData;

    private GameObject objIcon;
    private GameObject objName;
    private GameObject objHP;
    private GameObject objResource;
    private GameObject moreUnits;

    private void Start()
    {
        objIcon = unitMovementData.unitBlock.transform.GetChild(0).GetChild(0).gameObject;
        objName = unitMovementData.unitBlock.transform.GetChild(0).GetChild(1).gameObject;
        objHP = unitMovementData.unitBlock.transform.GetChild(0).GetChild(2).gameObject;
        objResource = unitMovementData.unitBlock.transform.GetChild(0).GetChild(3).gameObject;
        moreUnits = unitMovementData.unitBlock.transform.GetChild(1).GetChild(0).gameObject;
    }

    public void SetMultiUnitInfo()
    {
        unitMovementData.unitBlock.transform.GetChild(1).gameObject.SetActive(true);
        for (int i = 0; i < unitMovementData.unitsInSelection.Count; i++)
        {
            if (unitMovementData.unitsInSelection[i].name == "Villager(Clone)")
            {
                Transform childOfSlot = moreUnits.transform.GetChild(i);
                childOfSlot.GetChild(0).GetComponent<Image>().sprite =
                    unitMovementData.unitsInSelection[i].GetComponent<Builder>().icon;
                childOfSlot.GetChild(1).GetComponent<Slider>().maxValue =
                    unitMovementData.unitsInSelection[i].GetComponent<Builder>().maxHealth;
                childOfSlot.GetChild(1).GetComponent<Slider>().value =
                    unitMovementData.unitsInSelection[i].GetComponent<Builder>().health;
                childOfSlot.gameObject.SetActive(true);
            } else if (unitMovementData.unitsInSelection[i].name == "Archer(Clone)")
            {
                Transform childOfSlot = moreUnits.transform.GetChild(i);
                childOfSlot.GetChild(0).GetComponent<Image>().sprite =
                    unitMovementData.unitsInSelection[i].GetComponent<AttackUnit>().icon;
                childOfSlot.GetChild(1).GetComponent<Slider>().maxValue =
                    unitMovementData.unitsInSelection[i].GetComponent<AttackUnit>().maxHealth;
                childOfSlot.GetChild(1).GetComponent<Slider>().value =
                    unitMovementData.unitsInSelection[i].GetComponent<AttackUnit>().health;
                childOfSlot.gameObject.SetActive(true);
            }
        }
        unitMovementData.unitsInSelection.Clear();
    }

    public void SetUnitInfo()
    {
        string nameOfUnit = unitMovementData.unit.name;

        unitMovementData.btnBlock.transform.GetChild(0).gameObject.SetActive(true);
        unitMovementData.btnBlock.transform.GetChild(1).gameObject.SetActive(true);
        unitMovementData.unitBlock.transform.GetChild(0).gameObject.SetActive(true);
        unitMovementData.unitBlock.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        unitMovementData.unitBlock.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        unitMovementData.unitBlock.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        unitMovementData.unitBlock.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);

        if (nameOfUnit == "Villager(Clone)")
        {
            unitMovementData.btnBlock.transform.GetChild(1).GetChild(2).GetChild(0).gameObject.SetActive(true);
            unitMovementData.btnBlock.transform.GetChild(1).GetChild(2).GetChild(1).gameObject.SetActive(false);
            unitMovementData.btnBlock.transform.GetChild(1).GetChild(3).GetChild(0).gameObject.SetActive(true);
            unitMovementData.btnBlock.transform.GetChild(1).GetChild(3).GetChild(1).gameObject.SetActive(false);
            unitMovementData.btnBlock.transform.GetChild(0).gameObject.SetActive(true);
        } else
        {
            unitMovementData.btnBlock.transform.GetChild(1).GetChild(2).GetChild(0).gameObject.SetActive(false);
            unitMovementData.btnBlock.transform.GetChild(1).GetChild(2).GetChild(1).gameObject.SetActive(true);
            unitMovementData.btnBlock.transform.GetChild(1).GetChild(3).GetChild(0).gameObject.SetActive(false);
            unitMovementData.btnBlock.transform.GetChild(1).GetChild(3).GetChild(1).gameObject.SetActive(true);
            unitMovementData.btnBlock.transform.GetChild(0).gameObject.SetActive(false);
        }

        switch (nameOfUnit)
        {
            case "Villager(Clone)":
                Builder builderScript = unitMovementData.unit.GetComponent<Builder>();
                objIcon.GetComponent<Image>().sprite = builderScript.icon;
                objName.GetComponent<TMP_Text>().text = builderScript.name;
                objHP.GetComponent<Slider>().value = builderScript.health;
                objHP.transform.GetChild(2).GetComponent<TMP_Text>().text = builderScript.health.ToString()+"/"+builderScript.maxHealth.ToString();
                break;
            case "Archer(Clone)":
                AttackUnit attackScript = unitMovementData.unit.GetComponent<AttackUnit>();
                objIcon.GetComponent<Image>().sprite = attackScript.icon;
                objName.GetComponent<TMP_Text>().text = attackScript.name;
                objHP.GetComponent<Slider>().value = attackScript.health;
                objHP.transform.GetChild(2).GetComponent<TMP_Text>().text = attackScript.health.ToString()+"/"+attackScript.maxHealth.ToString();
                break;
        }
    }
}
