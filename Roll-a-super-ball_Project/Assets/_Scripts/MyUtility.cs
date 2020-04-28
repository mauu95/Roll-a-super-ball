using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyUtility
{
    public static void SetRotationZ(GameObject obj, float rot)
    {
        Vector3 r = obj.transform.rotation.eulerAngles;
        obj.transform.rotation = Quaternion.Euler(new Vector3(r.x, r.y, rot));
    }
}

