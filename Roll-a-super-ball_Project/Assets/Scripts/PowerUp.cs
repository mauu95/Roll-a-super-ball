using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour {
    protected PlayerController player;
    public string id;
    public float cooldownTime;
    private bool isCooldown;

    public delegate void onActivatePowerUp();
    public onActivatePowerUp onActivatePowerUpCallback;

    private void Start() {
        player = GetComponent<PlayerController>();
    }

    protected void Update() {
        if (!isCooldown && Input.GetKeyDown(KeyCode.Space)) {
            doStuff();
            if (onActivatePowerUpCallback != null)
                onActivatePowerUpCallback.Invoke();
            StartCoroutine(Cooldown());
        }
    }

    public abstract void doStuff();

    private IEnumerator Cooldown() {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

}
