using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public float MouseHorizontalSpeed = 2.0f;
    public float MouseVerticalSpeed = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void LateUpdate()
    {
        transform.position = player.transform.position;

        yaw += MouseHorizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= MouseVerticalSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
