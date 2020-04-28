using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMover : MonoBehaviour
{
    public float speed = 1;
    public float range = 1;

    private float offset;

    private void Start()
    {
        offset = transform.localPosition.y;
    }

    void Update()
    {
        transform.localPosition = new Vector3(0f, offset + Mathf.Sin(Time.time * speed) * range, 0f);
    }
}
