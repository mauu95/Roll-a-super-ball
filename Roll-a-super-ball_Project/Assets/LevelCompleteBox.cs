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
        animator.SetBool("show", true);
    }
}
