using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  
        {
            if(collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > collision.gameObject.GetComponent<PlayerController>().maxSpeedFromInput - 5)
                StartCoroutine(DestroyAfterSec(0.1f));
        }
    }

    IEnumerator DestroyAfterSec(float ttl)
    {
        yield return new WaitForSeconds(ttl);
        Destroy(gameObject);
    }
}
