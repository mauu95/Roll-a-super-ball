using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPopJumpP2 : TutorialPopUp
{
    public GameObject map2;
    public GameObject inventoryUI;

    private void Start()
    {
        inventoryUI.SetActive(true);
        map2.SetActive(true);
    }

    private void Update()
    {
        if (player.GetComponent<PowerUpJump>().isActiveAndEnabled)
            End();
    }
}
