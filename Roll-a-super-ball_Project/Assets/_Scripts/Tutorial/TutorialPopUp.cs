using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialPopUp : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    public void End()
    {
        GetComponentInParent<TutorialManager>().Next();
    }
}
