using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvaSuoni : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            GetComponent<AudioSource>().Play();
    }
}
