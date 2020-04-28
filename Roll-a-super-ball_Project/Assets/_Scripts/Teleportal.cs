using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportal : MonoBehaviour {
    public Teleportal otherPortal;

    [HideInInspector]
    public bool playerIsComing;

    public void Teleport(GameObject player, Teleportal portal)
    {
        portal.playerIsComing = true;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.position = portal.transform.position;
    }
}
