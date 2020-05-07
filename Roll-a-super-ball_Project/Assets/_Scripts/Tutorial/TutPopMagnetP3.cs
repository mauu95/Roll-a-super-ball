using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPopMagnetP3 : TutorialPopUp
{
    public GameObject bunchOfPickUps;

    void Start()
    {
        bunchOfPickUps.SetActive(true);
    }

    private void Update()
    {
        if (player.GetComponent<Inventory>().pickUps == 15)
            End();
    }
}
