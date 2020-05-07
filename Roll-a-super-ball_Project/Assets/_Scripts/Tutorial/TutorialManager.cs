using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<TutorialPopUp> popUps;
    public int popUpIndex;

    private void Start()
    {
        foreach(Transform child in transform)
        {
            TutorialPopUp temp = child.GetComponent<TutorialPopUp>();
            popUps.Add(temp);

        }
        popUps[popUpIndex].gameObject.SetActive(true);
    }

    public void Next()
    {
        popUps[popUpIndex].gameObject.SetActive(false);
        popUpIndex++;
        if (popUpIndex >= popUps.Count)
        {
            print("Tutorial Complete");
        }
        else
        {
            popUps[popUpIndex].gameObject.SetActive(true);
        }
    }
}
