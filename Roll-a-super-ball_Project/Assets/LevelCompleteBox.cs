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

    public void ToggleOn()
    {
        AudioManager.instance.Play("LevelComplete");
        GameManager.instance.CursorOn();
        animator.SetBool("show", true);
    }
}
