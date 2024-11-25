using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitCanvasController : MonoBehaviour
{
    [Header("Other scripts")]
    public UnitDataUI unitDataUI;
    public BuilderAction builderAction;
    public AttackUnitAction attackUnitAction;
    public ConstructionsNewBuildings constructionsNewBuildings;

    [Header("Obj in game")]
    public GameObject unitInfoObj;
    public GameObject unitActionObj;

    private GameObject builderBtns;
    private GameObject unitActionBtns;
    private List<Image> activeActionsBtns = new List<Image>();
    private List<Image> activeBuilderBtns = new List<Image>();
    private Image unitIconObj;
    private TMP_Text unitNameObj;
    private GameObject hpSliderObj;
    private GameObject buildingSliderObj;
    private Slider hpSlider;
    private TMP_Text hpText;
    private GameObject unitResources;
    private List<TMP_Text> unitResourcesText = new List<TMP_Text>();

    private void Awake()
    {
        builderBtns = unitActionObj.transform.GetChild(0).gameObject;
        unitActionBtns = unitActionObj.transform.GetChild(1).gameObject;
        
        for (int i = 0; i < unitActionBtns.transform.childCount; i++)
        {
            if (unitActionBtns.transform.GetChild(i).childCount > 0)
            {
                activeActionsBtns.Add(unitActionBtns.transform.GetChild(i).GetChild(0).GetComponent<Image>());
            }
        }
        for (int i = 0; i < builderBtns.transform.childCount; i++)
        {
            if (builderBtns.transform.GetChild(i).childCount > 0)
            {
                activeBuilderBtns.Add(builderBtns.transform.GetChild(i).GetChild(0).GetComponent<Image>());
            }
        }

        unitIconObj = unitInfoObj.transform.GetChild(0).GetComponent<Image>();
        unitNameObj = unitInfoObj.transform.GetChild(1).GetComponent<TMP_Text>();
        hpSliderObj = unitInfoObj.transform.GetChild(2).gameObject;
        buildingSliderObj = unitInfoObj.transform.GetChild(4).gameObject;
        hpSlider = hpSliderObj.GetComponent<Slider>();
        hpText = hpSliderObj.transform.GetChild(3).GetComponent<TMP_Text>();
        unitResources = unitInfoObj.transform.GetChild(3).gameObject;
        
        for (int i = 0; i < 3; i++)
        {
            unitResourcesText.Add(unitResources.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>());
        }

        unitInfoObj.SetActive(false);
        unitActionObj.SetActive(false);
    }

    public void SetupInfo(Type type, Sprite icon, string name, float maxHp, float hpNow)
    {
        unitInfoObj.SetActive(true);
        unitActionObj.SetActive(true);

        hpSliderObj.SetActive(true);
        buildingSliderObj.SetActive(false);

        SetupIconAndName(icon, name);
        SetupHp(maxHp, hpNow);
        SetupActionIcons(type);

        if (type != Type.Builder)
        {
            builderBtns.SetActive(false);
            unitResources.SetActive(false);
        }
        else
        {
            SetBuildersIcons();
            builderBtns.SetActive(true);
            unitResources.SetActive(true);
        }
    }

    public void BuilderResourcesSetup(GameObject unit, bool updateBtns)
    {
        List<UnitItems> unitItems = unit.GetComponent<UnitDataController>().items;
        Dictionary<string, int> resourceAmounts = new Dictionary<string, int>
        {
            { "wood", 0 }, { "stone", 0 }, { "food", 0 }
        };

        foreach (UnitItems unitItem in unitItems)
        {
            if (resourceAmounts.ContainsKey(unitItem.itemName))
            {
                resourceAmounts[unitItem.itemName] += unitItem.amount;
            }
        }

        unitResourcesText[0].text = resourceAmounts["wood"].ToString();
        unitResourcesText[1].text = resourceAmounts["stone"].ToString();
        unitResourcesText[2].text = resourceAmounts["food"].ToString();

        if (updateBtns)
        {
            builderAction.SetBuilderActionBtns(activeActionsBtns, unit);
        }
    }

    public void AttackUnitResourcesSetup(GameObject unit)
    {
        attackUnitAction.SetAttackUnitActionBtns(activeActionsBtns, unit);
    }

    private void SetBuildersIcons()
    {
        for (int j = 0; j < activeBuilderBtns.Count; j++)
        {
            activeBuilderBtns[j].sprite = unitDataUI.builderIcons[j];
            int index = j;
            activeBuilderBtns[j].transform.parent.GetComponent<Button>().onClick.AddListener(delegate { ConstructionBuildingByBuilder(index); });
        }
    }

    private void ConstructionBuildingByBuilder(int numOfHouse)
    {
        int index = numOfHouse;
        constructionsNewBuildings.ConstructionBuildings(index);
    }

    private void SetupActionIcons(Type type)
    {
        activeActionsBtns[0].sprite = unitDataUI.unitActionsIcons[0];

        if (type == Type.Builder)
        {
            activeActionsBtns[1].sprite = unitDataUI.unitActionsIcons[1];
            activeActionsBtns[2].sprite = unitDataUI.unitActionsIcons[4];
        } else
        {
            if (type == Type.Healer)
            {
                activeActionsBtns[1].sprite = unitDataUI.unitActionsIcons[3];
            }
            else
            {
                activeActionsBtns[1].sprite = unitDataUI.unitActionsIcons[2];
            }
            activeActionsBtns[2].sprite = unitDataUI.unitActionsIcons[5];
        }
    }

    private void SetupIconAndName(Sprite icon, string name)
    {
        unitIconObj.sprite = icon;
        unitNameObj.text = name;
    }

    private void SetupHp(float maxHp, float hpNow)
    {
        hpSlider.maxValue = maxHp;
        hpSlider.value = hpNow;

        UpdateHpText(maxHp, hpNow);
    }

    public void UpdateHpText(float maxHp, float hpNow)
    {
        hpText.text = $"{hpNow}/{maxHp}";
    }

    public void IncreaseResource(int num, int count)
    {
        unitResourcesText[num].text = count.ToString();
    }

    public void ClearUnitInfo()
    {
        unitInfoObj.SetActive(false);
        unitActionObj.SetActive(false); 
    }
}
