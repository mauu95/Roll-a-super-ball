using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteBox : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleOnOff()
    {
        GameManager.instance.TogglePause();
        animator.SetBool("show", true);
    }
}
