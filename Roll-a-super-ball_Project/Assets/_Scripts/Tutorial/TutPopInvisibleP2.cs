using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPopInvisibleP2 : TutorialPopUp
{
    private void Update()
    {
        if (player.GetComponent<PowerUpInvisible>().isActiveAndEnabled)
            End();
    }
}
