using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableMalus : MonoBehaviour {

    public string type;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (type == "ovalPlayer") {
                other.gameObject.AddComponent<OvalPlayerMalus>();
            }
            Destroy(gameObject);
        }
    }
}
