﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDash : PowerUp
{
    public float DashForce = 40f;
    public float dashDuration = 0.3f;

    private void Awake()
    {
        id = "dash";
        cooldownTime = 3;
    }

    public override void doStuff()
    {
        Vector3 direction;
        if (player.movement == Vector3.zero)
        {
            Vector3 forward = player.GetForwardDirection();
            direction = new Vector3(forward.x, 0f, forward.z);
        }
        else
            direction = player.movement;

        StartCoroutine(Dash(direction));
    }

    private IEnumerator Dash(Vector3 direction)
    {
        PlayerController p = player.GetComponent<PlayerController>();

        player.GetComponent<Rigidbody>().AddForce(direction * DashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(dashDuration);

        Vector3 newvel = p.GetComponent<Rigidbody>().velocity;
        newvel.y = 0;
        p.SetVelocity(newvel.normalized * 10);
    }

}
