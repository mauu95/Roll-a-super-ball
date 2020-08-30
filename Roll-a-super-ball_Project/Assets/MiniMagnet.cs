using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMagnet : MonoBehaviour
{
    public Transform follow;

    private void Update()
    {
        if(follow != null)
            transform.position = follow.position;
    }

    private void OnTriggerStay(Collider hit)
    {
        if (hit.transform.CompareTag("PickUp"))
        {
            Vector3 distance = transform.position - hit.transform.position;
            hit.transform.position += distance.normalized * 8f * Time.deltaTime;
        }
    }
}
