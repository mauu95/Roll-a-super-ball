using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraRotator : MonoBehaviour
{
    public float speed = 20f;

    private float radius;

    float posX, posY, angle;


    void Start()
    {
        radius = Mathf.Abs(transform.position.z);
    }

    void Update()
    {
        posX = Mathf.Cos(angle) * radius;
        posY = Mathf.Sin(angle) * radius;

        transform.position = new Vector3(posX, transform.position.y, posY);

        angle = angle + Time.deltaTime * speed;

        if (angle >= 360f)
            angle = 0f;

        transform.LookAt(Vector3.zero);
    }
}
