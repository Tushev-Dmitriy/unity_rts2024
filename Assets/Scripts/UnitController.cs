using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    private bool isMove = false;
    private Vector3 target;
    private NavMeshAgent agent;

    public GameObject tempUnit;
    public PanelController panelController;
    public bool isGame = false;
    public GameObject boxUI;
    public UnitSelection unitSelection;

    void Update()
    {
        if (isGame)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag == "Unit")
                {
                    tempUnit = hit.collider.gameObject;

                    panelController.unit = tempUnit;
                    panelController.SetUnitInfo();
                }
            }

            if (!unitSelection.isOneUnit)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (unitSelection.unitsInSelection != null)
                        {
                            for (int i = 0; i < unitSelection.unitsInSelection.Count; i++)
                            {
                                target = new Vector3(hit.point.x, unitSelection.unitsInSelection[i].transform.position.y,
                                    hit.point.z);
                                agent = unitSelection.unitsInSelection[i].GetComponent<NavMeshAgent>();
                                agent.SetDestination(target);
                            }
                            panelController.SetUnitInfo();
                        }
                        isMove = true;
                    }
                    boxUI.SetActive(true);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    boxUI.SetActive(false);
                }
            } else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        target = new Vector3(hit.point.x, tempUnit.transform.position.y,
                                    hit.point.z);
                        agent = tempUnit.GetComponent<NavMeshAgent>();
                        agent.SetDestination(target);
                        tempUnit.transform.GetChild(0).gameObject.SetActive(false);
                        isMove = true;
                        tempUnit = null;
                        unitSelection.isOneUnit = false;
                    }
                }
            }
        }
    }
}
