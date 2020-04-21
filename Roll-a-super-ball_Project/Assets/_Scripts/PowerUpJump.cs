using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpJump : PowerUp
{
    public float JumpForce = 100f;

    public override void doStuff()
    {
        float howDistantFromTheGroundCanIBeInOrderToJumpAnywaEvenyIfImNotTouchingWithTheGround; //If you read this you win a Grattino :)
        howDistantFromTheGroundCanIBeInOrderToJumpAnywaEvenyIfImNotTouchingWithTheGround = 0.2f + player.transform.localScale.y / 2;

        if (player.isGrounded || Physics.Raycast(transform.position, Vector3.down, howDistantFromTheGroundCanIBeInOrderToJumpAnywaEvenyIfImNotTouchingWithTheGround))
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
