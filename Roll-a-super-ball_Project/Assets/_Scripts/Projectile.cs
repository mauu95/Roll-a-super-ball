using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform p;

    private void Update()
    {
        transform.position = p.transform.position;
    }
}
