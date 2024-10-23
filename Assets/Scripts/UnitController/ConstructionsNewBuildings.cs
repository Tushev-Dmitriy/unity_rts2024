using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionsNewBuildings : MonoBehaviour
{
    [Header("User base obj")]
    public GameObject userBase;
    public GameObject[] buildingsObj;

    public void ConstructionBuildings(int numOfBuild)
    {
        GameObject tempBuilding = Instantiate(buildingsObj[numOfBuild], userBase.transform);
        Vector3 buildingScale = Vector3.zero;
        Vector3 buildingPos = Vector3.zero;

        switch (numOfBuild)
        {
            case 0:
                buildingScale = new Vector3(0.02f, 10, 0.02f);
                break;
            case 1:
                buildingScale = new Vector3(0.024f, 1.2f, 0.024f);
                break;
            case 2:
                buildingScale = new Vector3(0.03f, 15, 0.03f);
                break;
            case 3:
                buildingScale = new Vector3(0.0023f, 1.15f, 0.0023f);
                break;
            case 4:
                buildingScale = new Vector3(0.03f, 15, 0.03f);
                break;
            case 5:
                buildingScale = new Vector3(0.03f, 15, 0.03f);
                break;
            case 6:
                buildingScale = new Vector3(0.0017f, 0.88f, 0.0017f);
                break;
            case 7:
                buildingScale = new Vector3(0.0023f, 1.15f, 0.0023f);
                break;
            case 8:
                buildingScale = new Vector3(0.0023f, 1.15f, 0.0023f);
                break;
        }

        tempBuilding.transform.position = buildingPos;
        tempBuilding.transform.localScale = buildingScale;
    }
}
