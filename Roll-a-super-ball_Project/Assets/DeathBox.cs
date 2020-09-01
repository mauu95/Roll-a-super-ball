using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    public GameObject nextLevelButton;
    public GameObject[] child;

    public void FadeIn()
    {
        foreach(GameObject c in child)
            c.SetActive(true);

        GameManager.instance.CursorOn();

        if (!GameManager.instance.IsCurrentLevelPassed())
            nextLevelButton.SetActive(false);
    }
}
