using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPopJump : TutorialPopUp
{
    public GameObject pickableJumpPrefab;

    private void Start()
    {
        Instantiate(pickableJumpPrefab, Vector3.zero, Quaternion.identity);
    }

    private void Update()
    {
        if (player.GetComponent<PowerUpJump>())
            End();
    }
}
