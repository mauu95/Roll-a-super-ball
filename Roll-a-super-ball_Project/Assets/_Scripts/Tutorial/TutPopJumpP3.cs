using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPopJumpP3 : TutorialPopUp
{
    public Inventory player;

    private void Update()
    {
        if (player.pickUps == 5)
            End();
    }
}
