using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseForceEmitter : MonoBehaviour {
    public float force = 5f;

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("hello world 1");
        if (collision.gameObject.CompareTag("Player")) {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb) {
                rb.AddForce(0f, force, 0f, ForceMode.Impulse);
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log("hello world 2");
        if (other.gameObject.CompareTag("Player")) {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb) {
                rb.AddForce(0f, force, 0f, ForceMode.Impulse);
            }
        }
    }
}
