using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseForceEmitter : MonoBehaviour {
    public float force = 5f;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb) {
                rb.AddForce(0f, force, 0f, ForceMode.Impulse);
            }
        }
    }
}
