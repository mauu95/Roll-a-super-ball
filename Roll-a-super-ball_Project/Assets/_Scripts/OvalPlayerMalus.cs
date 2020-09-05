﻿using System;
using System.Collections;
using UnityEngine;

public class OvalPlayerMalus : MonoBehaviour {
    public Vector3 ovalSize = new Vector3(1, 1.5f, 0.5f);
    public float malusDuration = 10f;
    float Treshold = 24f;

    float soundTreshold = 10f;
    public string impactSound = "impactSound";

    private Rigidbody rb;
    private Vector3 oldVelocity;

    private Vector3 target;
    private Vector3 normalSize = new Vector3(1f, 1f, 1f);

    private bool malusEnabled = true;

    //So che stai leggendo cosa ho fatto... non l'ho capito nemmeno io. Se capisci, spiegamelo.. Grazie. 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = normalSize;
        GameManager.instance.CursorOff();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 newVelocity = rb.velocity;
        float delta = (oldVelocity - newVelocity).magnitude;

        if (delta > soundTreshold)
        {
            if (malusEnabled && delta > Treshold && transform.localScale == Vector3.one)
            {
                transform.localScale = ovalSize;
                target = ovalSize;

                AudioManager.instance.Play("ovalMalus");
                StartCoroutine(GetBackToNormalAfterSomeTime(malusDuration));
            }
            else
                AudioManager.instance.Play(impactSound);
        }


    }
    private void Update()
    {
        oldVelocity = rb.velocity;
        transform.localScale = Vector3.Lerp(transform.localScale, target, 0.0750f);
    }

    IEnumerator GetBackToNormalAfterSomeTime(float time)
    {
        yield return new WaitForSeconds(time);
        target = normalSize;
    }

    public void DisableMalus()
    {
        malusEnabled = false;
    }

    public void EnableMalus()
    {
        malusEnabled = true;
    }
}
