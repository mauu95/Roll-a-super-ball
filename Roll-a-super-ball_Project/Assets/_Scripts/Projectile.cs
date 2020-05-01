﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public Transform player;
    public ParticleSystem fire;

    public Color color;

    private void Start()
    {
        fire.startColor = color;
    }

    private void Update()
    {
        if (player == null)
            return;

        transform.position = player.transform.position;

        Vector3 pointToLook;
        pointToLook = transform.position + player.GetComponent<Rigidbody>().velocity.normalized;
        pointToLook = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);

        transform.LookAt(pointToLook);
    }
}
