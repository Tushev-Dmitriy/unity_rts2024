using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuilderAction : MonoBehaviour
{
    public List<GameObject> builderActionBtns;
    public bool isGetResource = false;

    public void SetBuilderActionBtns(List<Image> builderActionImg)
    {
        for (int i = 0; i < builderActionImg.Count; i++)
        {
            builderActionBtns.Add(builderActionImg[i].gameObject.transform.parent.gameObject);
        }

        builderActionBtns[2].GetComponent<Button>().onClick.AddListener(delegate { GetResource(); });
    }

    private void GetResource()
    {
        isGetResource = true;
    }
}
