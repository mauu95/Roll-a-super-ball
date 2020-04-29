using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int maxZoomIn = 9;
    public int maxZoomOut = -3;

    public int offset;
    void Update()
    {
        if (offset < maxZoomIn  && (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus)))
        {
            transform.localPosition += new Vector3(0, -1, 1);
            offset++;

        }
        if (offset > maxZoomOut && (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)))
        {
            transform.localPosition += new Vector3(0, 1, -1);
            offset--;
        }
    }
}
