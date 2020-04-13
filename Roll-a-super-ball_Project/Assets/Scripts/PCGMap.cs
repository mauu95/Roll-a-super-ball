using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PCGMap : MonoBehaviour {
    public int seed;
    public GameObject platformPrefab;
    public GameObject BrigdePrefab;
    public GameObject carloPrefab;
    public GameObject portalPrefab;
    public int Dimension = 20;
    public int nFloor = 1;
    public int nCarli;

    private IteratorSeed iseed;
    private List<Vector3> platforms;
    private List<TeleportPortal> portals;
    private GameObject map;

    private GameObject currentFloor;

    private void Start() {
        CreateMap();
    }

    private void CreateMap() {
        iseed = new IteratorSeed(seed);
        platforms = new List<Vector3>();
        portals = new List<TeleportPortal>();

        GameObject temp = new GameObject();
        map = Instantiate(temp);
        Destroy(temp);
        map.name = "Map";
        List<int> platformIndexes = new List<int>();
        for (int i = 0; i < nFloor; i++) {
            temp = new GameObject();
            currentFloor = Instantiate(temp, map.transform);
            Destroy(temp);
            currentFloor.name = "Floor" + i;

            transform.position = new Vector3(0f, 10f * i, 0f);
            CreateFloor();
            platformIndexes.Add(platforms.Count - 1);
        }

        // ignore last index
        for (int i = 0; i < platformIndexes.Count - 1; i++) {
            int index = platformIndexes[i];

            GameObject portalObject1 = Instantiate(portalPrefab, platforms[index] + Vector3.up, Quaternion.identity);
            TeleportPortal portal1 = portalObject1.GetComponent<TeleportPortal>();

            GameObject portalObject2 = Instantiate(portalPrefab, platforms[index + 1] + Vector3.up, Quaternion.identity);
            TeleportPortal portal2 = portalObject2.GetComponent<TeleportPortal>();

            portal1.otherPortal = portal2;
            portal2.otherPortal = portal1;
        }

        //Place Carli
        for (int i = 0; i < nCarli; i++) {
            Vector3 platPos = platforms[iseed.Next(platforms.Count)];
            Vector3 carloPos = new Vector3(platPos.x + iseed.Next(10) - 4, platPos.y + 1, platPos.z + iseed.Next(10) - 4);
            Create(carloPrefab, carloPos, Quaternion.identity);
        }

    }

    private void CreateFloor() {

        for (int i = 0; i < Dimension; i++)
            PerformAction();
    }

    private void PerformAction() {
        int action = iseed.Next(3);

        if (action == 0) {
            if (!platforms.Contains(transform.position)) {
                platforms.Add(transform.position);
                Create(platformPrefab, transform.position, transform.rotation);
            }
        } else if (action == 1) {
            int newdirection = iseed.Next(4);
            transform.Rotate(0f, newdirection * 90f, 0f);
        } else if (action == 2) {
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

    private GameObject Create(GameObject obj, Vector3 pos, Quaternion rot) {
        return Instantiate(obj, pos, rot, currentFloor.transform);
    }


}
