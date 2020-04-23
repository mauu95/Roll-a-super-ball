using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGHistoryStep
{
    public int action;
    public GameObject obj;

    public PCGHistoryStep(int action, GameObject obj = null)
    {
        this.action = action;
        this.obj = obj;
    }

    public void SetObj(GameObject obj)
    {
        this.obj = obj;
    }
}
