using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    public Enemy main;

    private void OnTriggerEnter(Collider other)
    {
        main.SomethingSpotted(other);
    }

    private void OnTriggerStay(Collider other)
    {
        main.SomethingSpotted(other);
    }
}
