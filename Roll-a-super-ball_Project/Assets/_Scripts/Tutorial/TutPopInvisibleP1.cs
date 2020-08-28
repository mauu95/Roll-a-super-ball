using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPopInvisibleP1 : TutorialPopUp
{
    public GameObject pickableInvisiblePrefab;

    private void Start()
    {
        Instantiate(pickableInvisiblePrefab, new Vector3(5f, 0.8f, 25f), Quaternion.identity);
    }

    private void Update()
    {
        if (player.GetComponent<PowerUpInvisible>())
            End();
    }
}
