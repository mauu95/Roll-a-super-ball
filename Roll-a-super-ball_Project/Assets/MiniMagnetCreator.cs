using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMagnetCreator : MonoBehaviour
{
    public GameObject miniMagnet;

    private void Start()
    {
        GameObject x = Instantiate(miniMagnet);
        x.GetComponent<MiniMagnet>().follow = transform;
    }
}
