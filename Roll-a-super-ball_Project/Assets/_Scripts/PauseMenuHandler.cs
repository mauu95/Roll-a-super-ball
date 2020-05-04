using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour {
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleOnOff();


    }

    private void ToggleOnOff()
    {
        GameManager.instance.TogglePause();
        animator.SetBool("show", GameManager.instance.IsPause);
    }
}
