using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public GameObject unit;

    public GameObject btnBlock;
    public GameObject unitBlock;

    public UnitSelection unitSelection;

    public void SetMultiUnitInfo()
    {
        GameObject moreUnits = unitBlock.transform.GetChild(1).GetChild(0).gameObject;
        for (int i = 0; i < unitSelection.unitsInSelection.Count; i++)
        {
            if (unitSelection.unitsInSelection[i].name == "Villager(Clone)")
            {
                Transform childOfSlot = moreUnits.transform.GetChild(i);
                childOfSlot.GetChild(0).GetComponent<Image>().sprite = 
                    unitSelection.unitsInSelection[i].GetComponent<Builder>().icon;
                childOfSlot.GetChild(1).GetComponent<Slider>().maxValue = 
                    unitSelection.unitsInSelection[i].GetComponent<Builder>().maxHealth;
                childOfSlot.GetChild(1).GetComponent<Slider>().value =
                    unitSelection.unitsInSelection[i].GetComponent<Builder>().health;
                childOfSlot.gameObject.SetActive(true);
            } else if (unitSelection.unitsInSelection[i].name == "Archer(Clone)")
            {
                Transform childOfSlot = moreUnits.transform.GetChild(i);
                childOfSlot.GetChild(0).GetComponent<Image>().sprite =
                    unitSelection.unitsInSelection[i].GetComponent<AttackUnit>().icon;
                childOfSlot.GetChild(1).GetComponent<Slider>().maxValue =
                    unitSelection.unitsInSelection[i].GetComponent<AttackUnit>().maxHealth;
                childOfSlot.GetChild(1).GetComponent<Slider>().value =
                    unitSelection.unitsInSelection[i].GetComponent<AttackUnit>().health;
                childOfSlot.gameObject.SetActive(true);
            }
        }
    }

    public void SetUnitInfo()
    {
        GameObject objIcon = unitBlock.transform.GetChild(0).GetChild(0).gameObject;
        GameObject objName = unitBlock.transform.GetChild(0).GetChild(1).gameObject;
        GameObject objHP = unitBlock.transform.GetChild(0).GetChild(2).gameObject;
        GameObject objResource = unitBlock.transform.GetChild(0).GetChild(3).gameObject;

        string nameOfUnit = unit.name;

        btnBlock.transform.GetChild(0).gameObject.SetActive(true);
        btnBlock.transform.GetChild(1).gameObject.SetActive(true);
        unitBlock.transform.GetChild(0).gameObject.SetActive(true);
        unitBlock.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        unitBlock.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        unitBlock.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        unitBlock.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);

        if (nameOfUnit == "Villager(Clone)")
        {
            btnBlock.transform.GetChild(1).GetChild(2).GetChild(0).gameObject.SetActive(true);
            btnBlock.transform.GetChild(1).GetChild(2).GetChild(1).gameObject.SetActive(false);
            btnBlock.transform.GetChild(1).GetChild(3).GetChild(0).gameObject.SetActive(true);
            btnBlock.transform.GetChild(1).GetChild(3).GetChild(1).gameObject.SetActive(false);
            btnBlock.transform.GetChild(0).gameObject.SetActive(true);
        } else
        {
            btnBlock.transform.GetChild(1).GetChild(2).GetChild(0).gameObject.SetActive(false);
            btnBlock.transform.GetChild(1).GetChild(2).GetChild(1).gameObject.SetActive(true);
            btnBlock.transform.GetChild(1).GetChild(3).GetChild(0).gameObject.SetActive(false);
            btnBlock.transform.GetChild(1).GetChild(3).GetChild(1).gameObject.SetActive(true);
            btnBlock.transform.GetChild(0).gameObject.SetActive(false);
        }

        switch (nameOfUnit)
        {
            case "Villager(Clone)":
                Builder builderScript = unit.GetComponent<Builder>();
                objIcon.GetComponent<Image>().sprite = builderScript.icon;
                objName.GetComponent<TMP_Text>().text = builderScript.name;
                objHP.GetComponent<Slider>().value = builderScript.health;
                objHP.transform.GetChild(2).GetComponent<TMP_Text>().text = builderScript.health.ToString()+"/"+builderScript.maxHealth.ToString();
                break;
            case "Archer(Clone)":
                AttackUnit attackScript = unit.GetComponent<AttackUnit>();
                objIcon.GetComponent<Image>().sprite = attackScript.icon;
                objName.GetComponent<TMP_Text>().text = attackScript.name;
                objHP.GetComponent<Slider>().value = attackScript.health;
                objHP.transform.GetChild(2).GetComponent<TMP_Text>().text = attackScript.health.ToString()+"/"+attackScript.maxHealth.ToString();
                break;
        }
    }
}
