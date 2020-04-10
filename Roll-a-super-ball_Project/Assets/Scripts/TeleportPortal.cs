using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPortal : MonoBehaviour {
    public TeleportPortal otherPortal;
    private bool playerIsComing = false;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && !playerIsComing) {
            if (!playerIsComing) {
                otherPortal.TeleportingHere();
                Rigidbody rb = other.GetComponent<Rigidbody>();
                rb.velocity = otherPortal.gameObject.transform.forward * rb.velocity.magnitude;
                other.gameObject.transform.position = otherPortal.gameObject.transform.position;
            } else {
                playerIsComing = false;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        playerIsComing &= !other.gameObject.CompareTag("Player");
    }

    public void TeleportingHere() {
        playerIsComing = true;
    }
}
