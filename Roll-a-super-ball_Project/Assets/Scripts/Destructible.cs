using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player && player.GetComponent<Rigidbody>().velocity.magnitude > player.maxSpeedFromInput - 5)  
            StartCoroutine(DestroyAfterSec(0.1f));
    }

    IEnumerator DestroyAfterSec(float ttl)
    {
        yield return new WaitForSeconds(ttl);
        Destroy(gameObject);
    }
}
