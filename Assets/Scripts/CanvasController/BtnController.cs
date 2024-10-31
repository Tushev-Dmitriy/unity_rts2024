using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    public GameObject objForBtn;

    public void SetupWindow()
    {
        if (objForBtn.activeSelf) 
        {
            objForBtn.SetActive(false);
        } else
        {
            objForBtn.SetActive(true);
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
