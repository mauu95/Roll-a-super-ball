using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PCGMap : MonoBehaviour
{
    public int seed;
    public GameObject platformPrefab;
    public GameObject BrigdePrefab;
    public int Dimension = 20;
    public int nFloor = 1;

    private IteratorSeed iseed;
    private List<Vector3> platforms;
    private GameObject map;

    private GameObject currentFloor;

    private void Start()
    {
        CreateMap();
    }

    private void CreateMap()
    {
        iseed = new IteratorSeed(seed);
        platforms = new List<Vector3>();

        GameObject temp = new GameObject();
        map = Instantiate(temp);
        Destroy(temp);
        map.name = "Map";

        for (int i = 0; i < nFloor; i++)
        {
            temp = new GameObject();
            currentFloor = Instantiate(temp, map.transform);
            Destroy(temp);
            currentFloor.name = "Floor" + i;

            transform.position = new Vector3(0f, 10f * i, 0f);
            CreateFloor();
        }

    }

    private void CreateFloor()
    {

        for (int i = 0; i < Dimension; i++)
            PerformAction();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PerformAction();

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }

    private void PerformAction()
    {
        int action = iseed.Next(3);

        if (action == 0)
        {
            if (!platforms.Contains(transform.position))
            {
                platforms.Add(transform.position);
                Create(platformPrefab, transform.position, transform.rotation);
            }
        }
        else if (action == 1)
        {
            int newdirection = iseed.Next(4);
            transform.Rotate(0f, newdirection * 90f, 0f);
        }
        else if (action == 2)
        {
            Vector3 prev = transform.position;
            int distance = 5 + iseed.Next(2) * 10 + iseed.Next(9);

            transform.position += transform.forward * distance;
            //place a bridge
            Vector3 curr = transform.position;

            Vector3 pos = (curr - prev) / 2 + prev;
            GameObject temp = Create(BrigdePrefab, pos, transform.rotation);

            Vector3 scale = temp.transform.localScale;
            temp.transform.localScale = new Vector3(scale.x, scale.y, scale.z * distance);
        }
    }

    private GameObject Create(GameObject obj, Vector3 pos, Quaternion rot)
    {
        return Instantiate(obj, pos, rot, currentFloor.transform);
    }


}
