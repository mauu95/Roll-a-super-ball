using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportalGFX : MonoBehaviour
{
    public Teleportal portal;

    private void OnTriggerEnter(Collider other)
    {
        GameObject player = other.gameObject;

        if (player.CompareTag("Player") && !portal.playerIsComing)
            portal.Teleport(player, portal.otherPortal);
    }

    private void OnTriggerExit(Collider other)
    {
        portal.playerIsComing &= !other.gameObject.CompareTag("Player");
    }
}
