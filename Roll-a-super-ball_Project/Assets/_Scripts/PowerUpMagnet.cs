using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMagnet : PowerUp {
    public float activeTime = 5;
    public float speed = 10;
    public float range = 10;

    private float nextTimeToDeactivate = 0;

    private void Awake() {
        id = "magnet";
        cooldownTime = 5;
    }

    protected new void Update() {
        base.Update();
        if (Time.realtimeSinceStartup < nextTimeToDeactivate) {
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

    public override void doStuff() {
        if (Time.realtimeSinceStartup >= nextTimeToDeactivate)
            nextTimeToDeactivate = Time.realtimeSinceStartup + activeTime;
    }


}
