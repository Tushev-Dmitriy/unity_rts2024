using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanelController : MonoBehaviour
{
    public int[] infoCount = new int[5];
    public List<TMP_Text> userInfoText;
    public GameObject userTownHall;
    private List<BuildingItems> buildingItems;

    public void CheckItemInTownHall()
    {
        buildingItems = userTownHall.GetComponent<BuildingDataController>().items;
    }

    public IEnumerator UpdateInfoText(int numOfInfo, int count, bool increase)
    {
        int tempCount = count;

        if (increase)
        {
            userInfoText[numOfInfo].color = Color.green;
            for (int i = tempCount; i > 0; i--)
            {
                int countNow = int.Parse(userInfoText[numOfInfo].text);
                userInfoText[numOfInfo].text = (countNow + 1).ToString();
                yield return new WaitForSeconds(0.2f);
            }
            userInfoText[numOfInfo].color = Color.white;
        } else
        {
            userInfoText[numOfInfo].color = Color.red;
            for (int i = tempCount; i > 0; i--)
            {
                int countNow = int.Parse(userInfoText[numOfInfo].text);
                userInfoText[numOfInfo].text = (countNow - 1).ToString();
                yield return new WaitForSeconds(0.2f);
            }
            userInfoText[numOfInfo].color = Color.white;
        }
    }
}
