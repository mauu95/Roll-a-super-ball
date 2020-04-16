using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    protected PlayerController player;
    public string id;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }
}
