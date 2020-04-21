using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpJump : PowerUp
{
    public float JumpForce = 100f;

    public override void doStuff()
    {
        if (player.isGrounded)
        {
            player.isGrounded = false;
            player.GetComponent<Rigidbody>().AddForce(0f, JumpForce, 0f, ForceMode.Impulse);
        }
    }

    private void Awake()
    {
        id = "jump";
        cooldownTime = 5f;
    }


}
