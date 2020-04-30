using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public float transitionSpeed = 1;
    public Transform follow;
    public PowerUpMagnet magnet;
    public float flickeringSpeed = 20;

    public Vector3 targetDim;

    private void Update()
    {
        Vector3 noise = Vector3.one * Mathf.Round(Mathf.Sin(Time.time * flickeringSpeed));
        transform.localScale = Vector3.Lerp(transform.localScale, targetDim + noise, Time.deltaTime * transitionSpeed);

        if (follow)
            transform.position = follow.position;
    }

    public void fadeIn(Vector3 dim)
    {
        targetDim = dim;
    }

    public void fadeOut()
    {
        targetDim = Vector3.zero;
    }

    private void OnTriggerStay(Collider other)
    {
        if(magnet && magnet.enabled)
            magnet.Attract(other.gameObject);
    }
}
