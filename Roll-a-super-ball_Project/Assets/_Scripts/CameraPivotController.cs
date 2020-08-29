using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotController : MonoBehaviour
{
    public GameObject player;

    float MouseHorizontalSpeed = 100f;
    float MouseVerticalSpeed = 100f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float smoothSpeed = 0.125f;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = player.transform.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        transform.position = smoothedPosition;

        yaw += Time.deltaTime * MouseHorizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= Time.deltaTime * MouseVerticalSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
