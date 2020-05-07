using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TutorialPopUp[] popUps;
    public int popUpIndex;

    private void Start()
    {
        popUps[popUpIndex].gameObject.SetActive(true);
    }

    public void Next()
    {
        popUps[popUpIndex].gameObject.SetActive(false);
        popUpIndex++;
        if (popUpIndex >= popUps.Length)
        {
            print("Tutorial Complete");
        }
        else
        {
            popUps[popUpIndex].gameObject.SetActive(true);
        }
    }
}
