using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    protected PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    public abstract string GetName();
}
