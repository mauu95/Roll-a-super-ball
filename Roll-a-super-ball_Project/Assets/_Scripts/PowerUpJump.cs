using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpJump : PowerUp
{
    public float JumpForce = 120f;

    public override void doStuff()
    {
        float howDistantFromTheGroundCanIBeInOrderToJumpAnywaEvenyIfImNotTouchingWithTheGround; //If you read this you win a Grattino :)
        howDistantFromTheGroundCanIBeInOrderToJumpAnywaEvenyIfImNotTouchingWithTheGround = 0.2f + player.transform.localScale.y / 2;

        if (player.isGrounded || Physics.Raycast(transform.position, Vector3.down, howDistantFromTheGroundCanIBeInOrderToJumpAnywaEvenyIfImNotTouchingWithTheGround))
        {
            player.isGrounded = false;

            Vector3 forward = GetComponent<PlayerController>().GetForwardDirection().normalized / 3;
            player.GetComponent<Rigidbody>().AddForce(forward.x * JumpForce, JumpForce, forward.z*JumpForce, ForceMode.Impulse);
        }
    }

    public override void ReturnToNormal()
    {

    }

    private void Awake()
    {
        id = "jump";
        cooldownTime = 2f;
    }


}
