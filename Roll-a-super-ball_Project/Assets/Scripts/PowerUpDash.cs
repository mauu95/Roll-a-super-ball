using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDash : PowerUp
{
    public float DashForce = 3f;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            player.GetComponent<Rigidbody>().AddForce(player.movement * DashForce , ForceMode.Impulse);
        }
    }
}
