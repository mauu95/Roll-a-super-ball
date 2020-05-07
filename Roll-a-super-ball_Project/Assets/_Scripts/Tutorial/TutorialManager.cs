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
        popUps[++popUpIndex].gameObject.SetActive(true);
    }
}
