using UnityEngine;
using System;

public class IteratorSeed : MonoBehaviour
{
    int intseed;
    private System.Random rand;

    public IteratorSeed(int seed)
    {
        this.intseed = seed;
        if (seed == 0)
            intseed = UnityEngine.Random.Range(0, 10000000);
        rand = new System.Random(intseed);

    }

    public int Next(int module)
    {
        return rand.Next() % module;
    }
}
