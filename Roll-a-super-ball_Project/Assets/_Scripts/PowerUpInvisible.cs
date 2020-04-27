using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvisible : PowerUp
{
    public bool isInvisible;
    public float duration = 2f;

    private Material mat;

    private void Awake()
    {
        id = "invisible";
        cooldownTime = 5f;
        mat = GetComponent<MeshRenderer>().material;
    }

    public override void doStuff()
    {
        isInvisible = true;
        Color temp = mat.color;
        temp.a = 0.4f;
        mat.color = temp;

        StartCoroutine(ReturnNormalAfterTime(duration));
    }

    IEnumerator ReturnNormalAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        isInvisible = false;
        Color temp = mat.color;
        temp.a = 1f;
        mat.color = temp;
    }
}
