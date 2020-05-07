using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPopMovement : TutorialPopUp
{
    public GameObject Wmarker;
    public GameObject Amarker;
    public GameObject Smarker;
    public GameObject Dmarker;

    bool Wpressed;
    bool Apressed;
    bool Spressed;
    bool Dpressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Wpressed = true;
            Wmarker.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Apressed = true;
            Amarker.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Spressed = true;
            Smarker.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dpressed = true;
            Dmarker.SetActive(true);
        }

        if (Wpressed && Apressed && Spressed && Dpressed)
            GetComponentInParent<TutorialManager>().Next();
    }
}
