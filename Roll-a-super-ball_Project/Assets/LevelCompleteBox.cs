using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelCompleteBox : MonoBehaviour
{
    Animator animator;

    private bool isEnabled;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleOn()
    {
        AudioManager.instance.Play("LevelComplete");
        GameManager.instance.CursorOn();
        animator.SetBool("show", true);
        isEnabled = true;
    }

    public bool isOn()
    {
        return isEnabled;
    }
}
