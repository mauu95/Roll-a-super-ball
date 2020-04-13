using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            StartCoroutine(DestroyAfterSec(0.1f));
    }

    IEnumerator DestroyAfterSec(float ttl)
    {
        yield return new WaitForSeconds(ttl);
        Destroy(gameObject);
    }
}
