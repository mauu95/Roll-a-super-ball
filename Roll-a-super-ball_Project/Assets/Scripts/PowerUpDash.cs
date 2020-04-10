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
            if(player.movement == Vector3.zero)
            {
                Vector3 forward = player.GetForwardDirection() * DashForce;
                player.GetComponent<Rigidbody>().AddForce(forward.x, 0f, forward.z, ForceMode.Impulse);
            }
            else
                player.GetComponent<Rigidbody>().AddForce(player.movement * DashForce , ForceMode.Impulse);
        }
    }
}
