using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public float transitionSpeed = 1;
    public Transform follow;

    private Vector3 targetDim;

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetDim, Time.deltaTime * transitionSpeed);

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
}
