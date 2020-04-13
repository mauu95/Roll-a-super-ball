using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpJump : PowerUp
{
    public float JumpForce = 25f;

    private void FixedUpdate()
    {
        if (player.isGrounded && Input.GetKey(KeyCode.Space))
        {
            player.isGrounded = false;
            player.GetComponent<Rigidbody>().AddForce(0f, JumpForce, 0f, ForceMode.Impulse);
        }
    }
}
