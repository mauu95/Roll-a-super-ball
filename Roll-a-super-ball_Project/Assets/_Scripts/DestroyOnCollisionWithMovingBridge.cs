using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollisionWithMovingBridge : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BridgeMoving>())
            gameObject.SetActive(false);
    }
}
