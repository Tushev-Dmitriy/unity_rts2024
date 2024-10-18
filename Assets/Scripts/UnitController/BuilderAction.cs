using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuilderAction : MonoBehaviour
{
    public List<GameObject> builderActionBtns;
    public bool isMoving = false;
    public bool isBuilding = false;
    public bool isGetResource = false;
    public void SetBuilderActionBtns(List<Image> builderActionImg, GameObject unit)
    {
        builderActionBtns.Clear();
        isGetResource = unit.GetComponent<BuilderResource>().isGetResource;

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
        isMoving = true;
        unit.GetComponent<BuilderResource>().isMoving = true;
    }

    private void SetupBuilding(GameObject unit)
    {
        isBuilding = true;
        unit.GetComponent<BuilderResource>().isBuilding = true;
    }

    private void GetResource(GameObject unit)
    {
        isGetResource = true;
        unit.GetComponent<BuilderResource>().isGetResource = true;
    }

    public void StartGetResource(GameObject resource, GameObject unit)
    {
        StartCoroutine(GetResourceAction(resource, unit));
    }

    IEnumerator GetResourceAction(GameObject res, GameObject unit)
    {
        yield return new WaitForSeconds(2);
        
        if (res.name == "tree_resource_model(Clone)")
        {
            unit.GetComponent<BuilderResource>().IncreaseResource(0);
        } else if (res.name == "Rock(Clone)")
        {
            unit.GetComponent<BuilderResource>().IncreaseResource(1);
        }
        Destroy(res);
        unit.GetComponent<BuilderResource>().isGetResource = false;
        isGetResource = false;
    }
}
