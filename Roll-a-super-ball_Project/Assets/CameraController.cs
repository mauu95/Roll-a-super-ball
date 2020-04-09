using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        transform.localPosition += new Vector3(0, 1, -1) * -Input.mouseScrollDelta.y;
    }
}
