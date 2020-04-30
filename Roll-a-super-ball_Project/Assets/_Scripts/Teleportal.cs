using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportal : MonoBehaviour {
    public Teleportal otherPortal;
    public GameObject direction;

    [HideInInspector]
    public bool playerIsComing;

    public ParticleSystem particle;
    public Material ActivePortalDirectionMat;
    public MeshRenderer[] cones;
    private Material defaultMat;

    private void Start()
    {
        defaultMat = cones[0].material;
    }

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
        //Graphic Effect 
        var emission = particle.emission;
        var vel = particle.velocityOverLifetime;

        foreach (MeshRenderer rend in cones)
            rend.material = ActivePortalDirectionMat;
        emission.rateOverTime = 15;
        vel.y = 30;

        smat.FadeOut();



        while (smat.isFading)
            yield return 0;



        //Revert Graphic Effect
        foreach (MeshRenderer rend in cones)
            rend.material = defaultMat;
        emission.rateOverTime = 2;
        vel.y = 3;

        smat.FadeIn();

        //Logic
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.position = portal.transform.position;
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
