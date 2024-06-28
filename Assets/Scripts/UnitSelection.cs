using UnityEngine;
using UnityEngine.AI;

public class UnitSelection : MonoBehaviour
{
    public UnitMovementData unitMovementData;
    public GameObject tempUnit;
    public PanelController panelController;

    private Vector3 target;
    private NavMeshAgent agent;
    private Vector2 startPos;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (unitMovementData.isGame)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (unitMovementData.unitsInSelection.Count <= 1)
                    {
                        if (tempUnit != null)
                        {
                            tempUnit.transform.GetChild(0).gameObject.SetActive(false);
                        }

                        if (hit.collider.tag == "Unit")
                        {
                            tempUnit = hit.collider.gameObject;

                            unitMovementData.unit = tempUnit;
                            panelController.SetUnitInfo();
                            unitMovementData.isOneUnit = true;
                            tempUnit.transform.GetChild(0).gameObject.SetActive(true);
                        }
                    } else
                    {
                        for (int i = 0; i < unitMovementData.unitsInSelection.Count; i++)
                        {
                            target = new Vector3(hit.point.x, unitMovementData.unitsInSelection[i].transform.position.y,
                                hit.point.z);
                            agent = unitMovementData.unitsInSelection[i].GetComponent<NavMeshAgent>();
                            agent.SetDestination(target);
                        }
                    }
                }
            }

            if (!unitMovementData.isOneUnit)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        startPos = Input.mousePosition;
                        if (unitMovementData.unitsInSelection != null)
                        {
                            for (int i = 0; i < unitMovementData.unitsInSelection.Count; i++)
                            {
                                unitMovementData.unitsInSelection[i].transform.GetChild(0).gameObject.SetActive(false);
                            }
                            //unitMovementData.unitsInSelection.Clear();
                        }

                        unitMovementData.boxUI.SetActive(true);
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    ReleaseSelectionBox();
                    unitMovementData.boxUI.SetActive(false);
                    panelController.SetMultiUnitInfo();
                }

                if (Input.GetMouseButton(0))
                {
                    UpdateSelectionBox(Input.mousePosition);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        target = new Vector3(hit.point.x, tempUnit.transform.position.y, hit.point.z);
                        agent = tempUnit.GetComponent<NavMeshAgent>();
                        agent.SetDestination(target);
                    }
                }
            }
        }
    }

    void UpdateSelectionBox(Vector2 curMousePos)
    {
        if (!unitMovementData.selectionBox.gameObject.activeInHierarchy)
            unitMovementData.selectionBox.gameObject.SetActive(true);
        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;
        unitMovementData.selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        unitMovementData.selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    void ReleaseSelectionBox()
    {
        unitMovementData.selectionBox.gameObject.SetActive(false);
        Vector2 min = unitMovementData.selectionBox.anchoredPosition - (unitMovementData.selectionBox.sizeDelta / 2);
        Vector2 max = unitMovementData.selectionBox.anchoredPosition + (unitMovementData.selectionBox.sizeDelta / 2);

        for (int i = 0; i < unitMovementData.unitsInTownCenter.transform.childCount; i++)
        {
            GameObject unit = unitMovementData.unitsInTownCenter.transform.GetChild(i).gameObject;

            Vector3 screenPos = cam.WorldToScreenPoint(unit.transform.position);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                unit.transform.GetChild(0).gameObject.SetActive(true);
                unitMovementData.unitsInSelection.Add(unit);
                Debug.Log(unit.name);
            }
        }

        panelController.SetMultiUnitInfo();
    }
}
