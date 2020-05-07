using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialPopUp : MonoBehaviour
{
    public void End()
    {
        GetComponentInParent<TutorialManager>().Next();
    }
}
