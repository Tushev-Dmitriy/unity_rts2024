using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCanvasController : MonoBehaviour
{
    [Header("Other scripts")]
    public BuildingDataUI buildingDataUI;

    [Header("Obj in canvas")]
    public GameObject buildingInfoObj;
    public GameObject unitActionObj;

    private Image buildingIconObj;
    private TMP_Text buildingNameObj;
    private GameObject hpSliderObj;
    private GameObject buildingSliderObj;
    private Slider hpSlider;
    private Slider buildingSlider;
    private TMP_Text hpText;
    private TMP_Text buildingText;
    private GameObject buildingResources;
    private List<TMP_Text> buildingResourcesText = new List<TMP_Text>();

    private void Awake()
    {
        buildingIconObj = buildingInfoObj.transform.GetChild(0).GetComponent<Image>();
        buildingNameObj = buildingInfoObj.transform.GetChild(1).GetComponent<TMP_Text>();
        hpSliderObj = buildingInfoObj.transform.GetChild(2).gameObject;
        buildingSliderObj = buildingInfoObj.transform.GetChild(4).gameObject;
        hpSlider = hpSliderObj.GetComponent<Slider>();
        buildingSlider = buildingSliderObj.GetComponent<Slider>();  
        hpText = hpSliderObj.transform.GetChild(3).GetComponent<TMP_Text>();
        buildingText = buildingSliderObj.transform.GetChild(3).GetComponent<TMP_Text>();

        buildingResources = buildingInfoObj.transform.GetChild(3).gameObject;

        for (int i = 0; i < 3; i++)
        {
            buildingResourcesText.Add(buildingResources.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>());
        }
    }

    public void SetupInfo(BuildingType type, Sprite icon, string name, float maxHp, float hpNow, bool isBuilding)
    {
        buildingInfoObj.SetActive(true);
        unitActionObj.SetActive(false);

        SetupIconAndName(icon, name);
        SetupHp(maxHp, hpNow, isBuilding);
    }

    private void SetupIconAndName(Sprite icon, string name)
    {
        buildingIconObj.sprite = icon;
        buildingNameObj.text = name;
    }

    private void SetupHp(float maxHp, float hpNow, bool isBuilding)
    {
        if (isBuilding) 
        {
            hpSliderObj.SetActive(false);   
            buildingSliderObj.SetActive(true);
            buildingSlider.maxValue = maxHp;
            buildingSlider.value = hpNow;
        } else
        {
            hpSliderObj.SetActive(true);
            buildingSliderObj.SetActive(false);
            hpSlider.maxValue = maxHp;
            hpSlider.value = hpNow;
        }

        UpdateHpText(maxHp, hpNow, isBuilding);
    }

    public void UpdateHpText(float maxHp, float hpNow, bool isBuilding)
    {
        if (isBuilding)
        {
            buildingText.text = $"{hpNow}/{maxHp}";
        } else
        {
            hpText.text = $"{hpNow}/{maxHp}";
        }
    }

    public void SetupResourceData(GameObject building)
    {
        buildingResources.SetActive(true);

        List<BuildingItems> buildingItems = building.GetComponent<BuildingDataController>().items;
        Dictionary<string, int> resourceAmounts = new Dictionary<string, int>
        {
            { "wood", 0 }, { "stone", 0 }, { "food", 0 }
        };

        if (buildingItems != null )
        {
            foreach (BuildingItems buildingItem in buildingItems)
            {
                if (resourceAmounts.ContainsKey(buildingItem.itemName))
                {
                    resourceAmounts[buildingItem.itemName] += buildingItem.amount;
                }
            }
        }

        buildingResourcesText[0].text = resourceAmounts["wood"].ToString();
        buildingResourcesText[1].text = resourceAmounts["stone"].ToString();
        buildingResourcesText[2].text = resourceAmounts["food"].ToString();

        //if (updateBtns)
        //{
        //    builderAction.SetBuilderActionBtns(activeActionsBtns, unit);
        //}
    }
}
