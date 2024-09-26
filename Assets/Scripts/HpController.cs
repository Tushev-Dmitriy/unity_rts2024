using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    public GameObject hpSliderObj;
    public int hpNow;
    public int maxHp;

    private Slider hpSlider;
    private TMP_Text hpText;


    private void Start()
    {
        hpSlider = hpSliderObj.GetComponent<Slider>();
        hpText = hpSliderObj.transform.GetChild(3).GetComponent<TMP_Text>();
    }

    public void SetupHp()
    {
        hpSlider.maxValue = maxHp;
        hpSlider.value = hpNow;

        UpdateHpText();
    }

    public void UpdateHpText()
    {
        hpText.text = $"{hpNow}/{maxHp}";
    }
}
