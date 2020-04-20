using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMalus : MonoBehaviour {
    [Range(0, 1)]
    public float speedDefect = 0.8f;
    private float speedIncrease;
    // Start is called before the first frame update
    void Start() {
        speedIncrease = 2 - speedDefect;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<PlayerController>().AlterSpeeds(speedDefect);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<PlayerController>().AlterSpeeds(speedIncrease);
        }
    }

}
