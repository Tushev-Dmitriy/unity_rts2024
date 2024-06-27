using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public RectTransform selectionBox;
    public GameObject unitsInTownCenter;
    public List<GameObject> unitsInSelection;
    public UnitController unitController;
    public bool isOneUnit = false;

    private Vector2 startPos;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (unitController.tempUnit != null)
        {
            isOneUnit = true;
            unitController.tempUnit.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (!isOneUnit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
                for (int i = 0; i < unitsInSelection.Count; i++)
                {
                    unitsInSelection[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                unitsInSelection.Clear();
            }

            if (Input.GetMouseButtonUp(0))
            {
                ReleaseSelectionBox();
            }

            if (Input.GetMouseButton(0))
            {
                UpdateSelectionBox(Input.mousePosition);
            }
        }
    }

    void UpdateSelectionBox(Vector2 curMousePos)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);
        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        for (int i = 0; i < unitsInTownCenter.transform.childCount; i++)
        {
            GameObject unit = unitsInTownCenter.transform.GetChild(i).gameObject;

            Vector3 screenPos = cam.WorldToScreenPoint(unit.transform.position);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                unit.transform.GetChild(0).gameObject.SetActive(true);
                unitsInSelection.Add(unit);
            }
        }
    }
}
