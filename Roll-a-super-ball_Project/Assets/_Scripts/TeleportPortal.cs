using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPortal : MonoBehaviour {
    public TeleportPortal otherPortal;

    private bool playerIsComing;

    private void OnTriggerEnter(Collider other) {
        GameObject player = other.gameObject;

        if (player.CompareTag("Player") && !playerIsComing)
            Teleport(player, otherPortal);
    }

    private void Teleport(GameObject player, TeleportPortal portal)
    {
        portal.playerIsComing = true;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.position = portal.transform.position;
    }

    private void OnTriggerExit(Collider other) {
        playerIsComing &= !other.gameObject.CompareTag("Player");
    }
}
