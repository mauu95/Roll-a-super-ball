using System;
using System.Collections;
using UnityEngine;

public class OvalPlayerMalus : MonoBehaviour {
    public Vector3 ovalSize = new Vector3(1, 1.5f, 0.5f);
    public float malusDuration = 10f;
    public float Treshold = 16f;

    private Rigidbody rb;
    private Vector3 oldVelocity;

    //So che stai leggendo cosa ho fatto... non l'ho capito nemmeno io. Se capisci, spiegamelo.. Grazie. 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 newVelocity = rb.velocity;
        if ((oldVelocity - newVelocity).magnitude > Treshold)
        {
            rb.transform.localScale = ovalSize;
            StartCoroutine(GetBackToNormalAfterSomeTime(malusDuration));
        }
    }
    private void Update()
    {
        oldVelocity = rb.velocity;
    }


    IEnumerator GetBackToNormalAfterSomeTime(float time)
    {
        yield return new WaitForSeconds(time);
        rb.transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
