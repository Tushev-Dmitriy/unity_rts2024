using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitController : MonoBehaviour
{
    [Header("Other scripts")]
    public BuilderAction builderAction;

    [Header("Units pick")]
    public GameObject imgToPick;
    public GameObject tempUnit;

    public bool isBuild = false; // Флаг для режима строительства
    private bool isMove = false;
    private Vector3 target;
    private Rigidbody rb;
    private Vector3 clickPos = Vector3.zero;
    private Vector3 click2Pos = Vector3.zero;
    private Vector3 rotationPos = Vector3.zero;
    private Vector3 mousePos = Vector3.zero;
    private LineRenderer lineRenderer;

    private GameObject resourceToGet;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.tag == "UnitToMove")
                {
                    if (lineRenderer != null)
                    {
                        lineRenderer.enabled = false;
                    }

                    if (rb != null)
                    {
                        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                        tempUnit = null;
                    }
                    isMove = false;
                    tempUnit = hit.collider.gameObject;
                    clickPos = hit.point;
                    rotationPos = new Vector3();

                    hit.collider.gameObject.GetComponent<UnitDataController>().SetUnitsInfoInUI();
                    hit.collider.gameObject.GetComponent<UnitDataController>().SetBuilderResource(tempUnit);
                    lineRenderer = hit.collider.gameObject.GetComponent<LineRenderer>();
                    lineRenderer.enabled = true;
                }
                else if (hit.collider.tag == "BuildingToUser")
                {
                    if (lineRenderer != null)
                    {
                        lineRenderer.enabled = false;
                    }

                    isMove = false;
                    Transform currentTransform = hit.transform;

                    while (currentTransform != null)
                    {
                        BuildingDataController buildingDataController = currentTransform.GetComponent<BuildingDataController>();
                        if (buildingDataController != null)
                        {
                            buildingDataController.SetBuildingInfoInUI();
                            tempUnit = currentTransform.gameObject;
                            buildingDataController.SetupBuildingResource(tempUnit);
                            lineRenderer = currentTransform.gameObject.GetComponent<LineRenderer>();
                            lineRenderer.enabled = true;
                            break;
                        }
                        currentTransform = currentTransform.parent;
                    }
                }
            }
        }

        if (tempUnit != null)
        {
            if (isBuild)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    target = new Vector3(hit.point.x, tempUnit.transform.position.y, hit.point.z);
                    tempUnit.transform.position = target;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    NewBuildingController tempBuilding = tempUnit.GetComponent<NewBuildingController>();
                    if (tempBuilding.canBuild)
                    {
                        tempBuilding.SetMaterialToLine(0);
                        isBuild = false;
                        tempUnit.tag = "BuildingToUser";
                        Destroy(tempBuilding);
                    }
                }
            }
            else
            {
                BuilderResource builderResource = tempUnit.GetComponent<BuilderResource>();
                AttackUnitResource attackUnitResource = tempUnit.GetComponent<AttackUnitResource>();

                bool canMove = (builderResource != null && builderResource.isMoving) ||
                               (attackUnitResource != null && attackUnitResource.isMoving);

                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (canMove)
                        {
                            if (hit.collider.tag != "MapObject")
                            {
                                target = new Vector3(hit.point.x, tempUnit.transform.position.y, hit.point.z);
                                isMove = true;
                                click2Pos = hit.point;
                            }
                        }
                        else if (builderResource != null && builderResource.isGetResource && hit.collider.tag == "MapObject")
                        {
                            target = new Vector3(hit.point.x, tempUnit.transform.position.y, hit.point.z);
                            isMove = true;
                            click2Pos = hit.point;
                            resourceToGet = hit.collider.gameObject;
                        }
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            Vector3 targetPos = target;
            Vector3 currentPos = tempUnit.transform.position;

            NavMeshAgent agent = tempUnit.GetComponent<NavMeshAgent>();

            float dist = Vector3.Distance(currentPos, targetPos);

            rb = tempUnit.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            if (dist > 1f)
            {
                agent.destination = targetPos;
                tempUnit.transform.LookAt(targetPos);
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                agent.destination = currentPos;
                isMove = false;

                var builderResource = tempUnit.GetComponent<BuilderResource>();
                if (builderResource != null && builderResource.isGetResource)
                {
                    builderAction.StartGetResource(resourceToGet, tempUnit);
                }
            }
        }
    }
}