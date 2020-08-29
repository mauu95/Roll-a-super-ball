using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPopMagnetP1 : TutorialPopUp
{
    public GameObject magnetPrefab;

    private void Start()
    {
        Instantiate(magnetPrefab, new Vector3(0f, .8f, 40f), Quaternion.identity);
    }

    private void Update()
    {
        if (player.GetComponent<PowerUpMagnet>())
            End();
    }
}
