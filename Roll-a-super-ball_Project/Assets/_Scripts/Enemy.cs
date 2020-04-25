using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject platform;

    private NavMeshAgent agent;
    private float platformScale;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (platform)
            Patrol();
    }

    public void SetPlatform(GameObject plat)
    {
        platform = plat;
        platformScale = platform.transform.localScale.x;
    }

    private void Patrol()
    {
        if (agent.destination.x == transform.position.x && agent.destination.z == transform.position.z)
        {
            agent.SetDestination(GetRandomPointOnThePlatform());
            agent.isStopped = true;
            StartCoroutine(UnleashAfterDelay(1f));
        }

    }

    private Vector3 GetRandomPointOnThePlatform()
    {
        float x = Random.Range(0f, platformScale) - platformScale / 2;
        float z = Random.Range(0f, platformScale) - platformScale / 2;
        return platform.transform.position + new Vector3(x, 0f, z);
    }

    IEnumerator UnleashAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        agent.isStopped = false;
    }

}
