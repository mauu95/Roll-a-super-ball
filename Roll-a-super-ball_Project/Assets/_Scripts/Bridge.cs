using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bridge : MonoBehaviour
{
    public abstract void SetEndPoints(Vector3 start, Vector3 end);
}
