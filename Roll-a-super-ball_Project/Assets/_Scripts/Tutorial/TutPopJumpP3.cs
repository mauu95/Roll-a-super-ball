using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPopJumpP3 : TutorialPopUp
{
    private void Update()
    {
        if (player.GetComponent<Inventory>().pickUps == 5)
            End();
    }
}
