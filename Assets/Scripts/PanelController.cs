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

    public void SetUnitInfo()
    {
        GameObject objIcon = unitBlock.transform.GetChild(0).gameObject;
        GameObject objName = unitBlock.transform.GetChild(1).gameObject;
        GameObject objHP = unitBlock.transform.GetChild(2).gameObject;
        GameObject objResource = unitBlock.transform.GetChild(3).gameObject;

        string nameOfUnit = unit.name;

        btnBlock.transform.GetChild(0).gameObject.SetActive(true);
        btnBlock.transform.GetChild(1).gameObject.SetActive(true);
        unitBlock.transform.GetChild(0).gameObject.SetActive(true);
        unitBlock.transform.GetChild(1).gameObject.SetActive(true);
        unitBlock.transform.GetChild(2).gameObject.SetActive(true);
        unitBlock.transform.GetChild(3).gameObject.SetActive(true);

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
