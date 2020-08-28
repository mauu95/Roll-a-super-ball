using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPopJump : TutorialPopUp
{
    public GameObject pickableJumpPrefab;

    private void Start()
    {
        Instantiate(pickableJumpPrefab, new Vector3(0f, .8f, 0f), Quaternion.identity);
    }

    private void Update()
    {
        if (player.GetComponent<PowerUpJump>())
            End();
    }
}
