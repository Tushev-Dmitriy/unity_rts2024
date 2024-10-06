using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public GameObject checkMarkRed;
    public GameObject onText;
    public GameObject offText;

    public Toggle toggle;

    public void CheckToggle()
    {
        if (!toggle.isOn)
        {
            onText.SetActive(false);
            offText.SetActive(true);
            checkMarkRed.SetActive(true);
        } else if (toggle.isOn)
        {
            onText.SetActive(true);
            offText.SetActive(false);
            checkMarkRed.SetActive(false);
        }
    }
}
