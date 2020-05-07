using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPopMagnetP2 : TutorialPopUp
{
    private void Update()
    {
        if (player.GetComponent<PowerUpMagnet>().isActiveAndEnabled)
            End();
    }
}
