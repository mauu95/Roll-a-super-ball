﻿using UnityEngine;
using UnityEngine.AI;

public class CustomAgent : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit))
            {
                agent.SetDestination(hit.point);
            }
        }


    }
}
