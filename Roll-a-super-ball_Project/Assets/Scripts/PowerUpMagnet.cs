using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMagnet : PowerUp {
    public float speed = 10;
    public float range = 10;

    private void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, range, transform.forward);
            AttractHitObjects(hits);
        }
    }

    private void AttractHitObjects(RaycastHit[] hits) {
        foreach (RaycastHit hit in hits) {
            if (hit.transform.CompareTag("PickUp")) {
                Vector3 distance = transform.position - hit.transform.position;
                hit.transform.position += distance.normalized * speed * Time.deltaTime;
            }
        }

    }

}
