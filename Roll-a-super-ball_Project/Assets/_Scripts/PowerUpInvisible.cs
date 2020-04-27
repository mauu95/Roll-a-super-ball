using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvisible : PowerUp
{
    public bool isInvisible;
    public float duration = 2f;
    public float transitionDurationSpeed = 32;

    private Material mat;
    private Color invisible;
    private Color visible;
    private Color target;

    private void Awake()
    {
        id = "invisible";
        cooldownTime = 5f;
        mat = GetComponent<MeshRenderer>().material;

        visible = mat.color;
        invisible = mat.color;
        invisible.a = 0.1f;
        SetColor(visible);
    }

    public override void doStuff()
    {
        isInvisible = true;
        SetColor(invisible);
        StartCoroutine(ReturnNormalAfterTime(duration));
    }

    IEnumerator ReturnNormalAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        isInvisible = false;
        SetColor(visible);
    }

    private void SetColor(Color c)
    {
        target = c;
    }

    private void LateUpdate()
    {
        if(mat.color != target)
            mat.color = Color.Lerp(mat.color, target, 1/transitionDurationSpeed);
    }

    public override void ReturnToNormal()
    {
        isInvisible = false;
        mat.color = visible;
    }
}
