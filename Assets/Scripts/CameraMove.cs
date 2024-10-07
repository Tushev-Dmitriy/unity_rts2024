using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float camSpeed = 5f;
    public float rotationSpeed = 150f;

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.z -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += 1;
        }

        if (Input.GetMouseButton(2))
        {
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (transform.position.y > 5.2f && transform.position.y < 25.2f)
            {
                if (Input.GetAxis("Mouse ScrollWheel") == -0.1f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
                }

                if (Input.GetAxis("Mouse ScrollWheel") == 0.1f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);
                }
            }

            if (transform.position.y == 5.2f || transform.position.y == 5.200001f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            }
            else if (transform.position.y == 25.2f || transform.position.y == 25.200001f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
            }
        }

        movement = movement.normalized * camSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position + Quaternion.Euler(0, transform.eulerAngles.y, 0) * movement;
        newPosition.y = transform.position.y;

        transform.position = newPosition;
    }
}
