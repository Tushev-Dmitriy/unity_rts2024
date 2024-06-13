using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera camera;
    private bool isDown = false;
    private void Start()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        Debug.Log(Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            isDown = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            isDown = false;
        }

        if (isDown)
        {
            camera.transform.position = new Vector3(0, Input.GetAxis("Vertical"), -31.3f);
        }
    }
}
