using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuilderAction : MonoBehaviour
{
    public List<GameObject> builderActionBtns;

    public void SetBuilderActionBtns(List<Image> builderActionImg, GameObject unit)
    {
        builderActionBtns.Clear();

        for (int i = 0; i < builderActionImg.Count; i++)
        {
            builderActionBtns.Add(builderActionImg[i].gameObject.transform.parent.gameObject);
        }

        builderActionBtns[0].GetComponent<Button>().onClick.AddListener(delegate { SetupMove(unit); });
        builderActionBtns[1].GetComponent<Button>().onClick.AddListener(delegate { SetupBuilding(unit); });
        builderActionBtns[2].GetComponent<Button>().onClick.AddListener(delegate { GetResource(unit); });
    }

    private void SetupMove(GameObject unit)
    {
        BuilderResource builderResource = unit.GetComponent<BuilderResource>();
        bool currentFlag = builderResource.isMoving;

        ClearAllFlags(unit);
        builderResource.isMoving = !currentFlag;
    }

    private void SetupBuilding(GameObject unit)
    {
        BuilderResource builderResource = unit.GetComponent<BuilderResource>();
        bool currentFlag = builderResource.isBuilding;

        ClearAllFlags(unit);
        builderResource.isBuilding = !currentFlag;  
    }

    private void GetResource(GameObject unit)
    {
        BuilderResource builderResource = unit.GetComponent<BuilderResource>();
        bool currentFlag = builderResource.isGetResource;  

        ClearAllFlags(unit); 
        builderResource.isGetResource = !currentFlag; 
    }

    private void ClearAllFlags(GameObject unit)
    {
        BuilderResource builderResource = unit.GetComponent<BuilderResource>();
        builderResource.isMoving = false;
        builderResource.isBuilding = false;
        builderResource.isGetResource = false;
    }

    public void StartGetResource(GameObject resource, GameObject unit)
    {
        StartCoroutine(GetResourceAction(resource, unit));
    }

    IEnumerator GetResourceAction(GameObject res, GameObject unit)
    {
        if (res == null || unit == null)
        {
            yield break;
        }

        yield return new WaitForSeconds(2);

        if (res.name == "tree_resource_model(Clone)")
        {
            CheckItemsInUnit(unit, 0);
        }
        else if (res.name == "Rock(Clone)")
        {
            CheckItemsInUnit(unit, 1);
        }

        Destroy(res);
        unit.GetComponent<BuilderResource>().isGetResource = false;
        unit.GetComponent<BuilderResource>().UpdateBuilderResourcesCanvas();
        //GoToTownHall(unit);
    }

    private void GoToTownHall(GameObject unit)
    {
        Vector3 posToTownHall = new Vector3(0, unit.transform.localPosition.y, 14);
        NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
        Rigidbody rb = unit.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        agent.destination = posToTownHall;
        agent.Move(posToTownHall);
    }

    private void CheckItemsInUnit(GameObject unit, int numOfRes)
    {
        List<UnitItems> items = unit.GetComponent<UnitDataController>().items;
        UnitItems newItem = new UnitItems();

        if (numOfRes == 0)
        {
            newItem.itemName = "wood";
        }
        else if (numOfRes == 1)
        {
            newItem.itemName = "stone";
        }
        else if (numOfRes == 2)
        {
            newItem.itemName = "food";
        }

        bool isAdd = false;
        foreach (UnitItems item in items)
        {
            if (item.itemName == newItem.itemName)
            {
                item.amount++;
                isAdd = true;
            }
        }

        if (!isAdd)
        {
            newItem.amount = 1;
            items.Add(newItem);
        }
    }
}
