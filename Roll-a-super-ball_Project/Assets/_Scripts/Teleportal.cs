using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportal : MonoBehaviour {
    public Teleportal otherPortal;
    public GameObject direction;

    [HideInInspector]
    public bool playerIsComing;

    public void Teleport(GameObject player, Teleportal portal)
    {
        portal.playerIsComing = true;

        Smaterializator smat = player.GetComponent<Smaterializator>();

        if(smat == null)
        {
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.transform.position = portal.transform.position;
        }
        else
            StartCoroutine(ChagePositionAfterSmaterialization(player, smat, portal));
    }

    IEnumerator ChagePositionAfterSmaterialization(GameObject player, Smaterializator smat, Teleportal portal)
    {
        smat.FadeOut();

        while (smat.isFading)
            yield return 0;

        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.position = portal.transform.position;
        smat.FadeIn();
    }

    public void IsGoingUp()
    {
        MyUtility.SetRotationZ(direction, 0f);
    }

    public void IsGoingDown()
    {
        MyUtility.SetRotationZ(direction, 180f);
    }
}
