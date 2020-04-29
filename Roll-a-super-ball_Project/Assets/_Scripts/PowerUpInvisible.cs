using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvisible : PowerUp
{
    public bool isInvisible;
    public float duration = 2f;
    public float transitionDuration = 1;
    public Material invisibleMat;

    private Material defaultMat;
    private MeshRenderer rend;
    private Color invisible;
    private Color visible;
    private Color target;

    private void Awake()
    {
        id = "invisible";
        cooldownTime = 5f;

        rend = GetComponent<MeshRenderer>();
        invisibleMat = PrefabManager.instance.invisibleMaterial;
        defaultMat = rend.material;

        visible = invisibleMat.color;
        invisible = invisibleMat.color;
        invisible.a = 0.1f;

        target = visible;
    }

    private void LateUpdate()
    {
        if (invisibleMat.color != target)
            invisibleMat.color = Color.Lerp(invisibleMat.color, target, (Time.deltaTime) / transitionDuration * 2 );

        if (target == visible && invisibleMat.color.a > 0.9f)
            rend.material = defaultMat;
    }

    public override void doStuff()
    {
        isInvisible = true;
        SetInvisible();
        StartCoroutine(ReturnNormalAfterTime(duration));
    }

    IEnumerator ReturnNormalAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        isInvisible = false;
        SetVisible();
    }

    private void SetVisible()
    {
        target = visible;
    }

    private void SetInvisible()
    {
        rend.material = invisibleMat;
        target = invisible;
    }

    public override void ReturnToNormal()
    {
        isInvisible = false;
        invisibleMat.color = visible;
        rend.material = defaultMat;
    }
}
