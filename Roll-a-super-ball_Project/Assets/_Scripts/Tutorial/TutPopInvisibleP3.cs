using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutPopInvisibleP3 : TutorialPopUp
{
    public GameObject map3;
    public Platform plat;
    public NavMeshSurface navMesh;

    private void Start()
    {
        map3.SetActive(true);
        navMesh.BuildNavMesh();
        plat.CreateEnemy();
    }
}
