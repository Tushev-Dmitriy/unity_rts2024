using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitCanvasController : MonoBehaviour
{
    [Header("Other scripts")]
    public UnitDataUI unitDataUI;

    [Header("Obj in canvas")]
    public GameObject unitInfoObj;
    public GameObject unitActionObj;

    private GameObject builderBtns;
    private GameObject unitActionBtns;
    private List<Image> activeActionsBtns = new List<Image>();
    private List<Image> activeBuilderBtns = new List<Image>();
    private Image unitIconObj;
    private TMP_Text unitNameObj;
    private GameObject hpSliderObj;
    private Slider hpSlider;
    private TMP_Text hpText;

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
        hpSlider = hpSliderObj.GetComponent<Slider>();
        hpText = hpSliderObj.transform.GetChild(3).GetComponent<TMP_Text>();

        unitInfoObj.SetActive(false);
        unitActionObj.SetActive(false);
    }

    public void SetupInfo(Type type, Sprite icon, string name, float maxHp, float hpNow)
    {
        unitInfoObj.SetActive(true);
        unitActionObj.SetActive(true);

        SetupIconAndName(icon, name);
        SetupHp(maxHp, hpNow);
        SetupActionIcons(type);

        if (type != Type.Builder)
        {
            builderBtns.SetActive(false);
        }
        else
        {
            SetBuildersIcons();
            builderBtns.SetActive(true);
        }
    }

    private void SetBuildersIcons()
    {
        for (int j = 0; j < activeBuilderBtns.Count; j++)
        {
            activeBuilderBtns[j].sprite = unitDataUI.builderIcons[j];
        }
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

}
