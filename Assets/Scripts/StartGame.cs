using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject planeObj;

    public void StartSpawn()
    {
        Instantiate(planeObj, Vector3.zero, Quaternion.identity);
    }
}
