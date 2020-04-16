using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    protected PlayerController player;
    public string id;
    public float cooldownTime;
    private bool isCooldown;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!isCooldown && Input.GetKeyDown(KeyCode.Space))
        {
            doStuff();
            StartCoroutine(Cooldown());
        }
    }

    public abstract void doStuff();

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

}
