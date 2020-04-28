using System;
using System.Collections;
using UnityEngine;

public class OvalPlayerMalus : MonoBehaviour {
    public Vector3 ovalSize = new Vector3(1, 1.5f, 0.5f);
    public float malusDuration = 10f;
    public float Treshold = 16f;

    private Rigidbody rb;
    private Vector3 oldVelocity;

    private Vector3 target;
    private Vector3 normalSize = new Vector3(1f, 1f, 1f);

    //So che stai leggendo cosa ho fatto... non l'ho capito nemmeno io. Se capisci, spiegamelo.. Grazie. 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = normalSize;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 newVelocity = rb.velocity;
        if ((oldVelocity - newVelocity).magnitude > Treshold)
        {
            transform.localScale = ovalSize;
            target = ovalSize;

            StartCoroutine(GetBackToNormalAfterSomeTime(malusDuration));
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

}
