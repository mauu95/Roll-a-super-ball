using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeNormal : Bridge
{
    public override void SetEndPoints(Vector3 start, Vector3 end)
    {
        Vector3 pos = (end - start) / 2 + start;
        transform.position = pos;
        Vector3 scale = transform.localScale;
        float distance = (end - start).magnitude;
        transform.localScale = new Vector3(scale.x, scale.y, scale.z * distance + 3f);
    }
}

