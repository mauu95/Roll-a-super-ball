using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvisible : PowerUp
{
    public bool isInvisible;
    public float duration = 2f;

    private void Awake()
    {
        id = "invisible";
        cooldownTime = 5f;
    }

    public override void doStuff()
    {
        isInvisible = true;
        GetComponent<MeshRenderer>().material.color = Color.white;

        StartCoroutine(ReturnNormalAfterTime(duration));
    }

    IEnumerator ReturnNormalAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        isInvisible = false;
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
