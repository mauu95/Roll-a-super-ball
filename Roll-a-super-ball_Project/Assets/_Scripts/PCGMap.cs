﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PCGMap : MonoBehaviour {
    public int seed;
    public int Dimension = 20;
    public int nFloor = 1;
    public int brigdeLength = 25;
    public int nPickUps;
    public GameObject[] platformPrefabs;
    public GameObject[] BrigdePrefab;
    public CoupleGameobjectInt[] elementToAddOnMap;
    public GameObject portalPrefab;
    [HideInInspector]
    public PCGHistory history;


    private IteratorSeed iseed;
    private List<GameObject> platforms;
    private List<int> platformIndexes;
    private GameObject map;
    private int[] platformsSize = new int[] { 16, 20, 24 };
    private int bridgeMinimumLength;

    private GameObject currentFloor;

    private void Start() {
        if (elementToAddOnMap[0].prefab.name == "PickUp") elementToAddOnMap[0].quantity = GameManager.instance.nPickUp;
        platformIndexes = new List<int>();
        history = new PCGHistory();

        CreateMap();
    }

    private void CreateMap()
    {
        iseed = new IteratorSeed(seed);
        platforms = new List<GameObject>();
        map = CreateEmptyGameObject("Map");

        for (int i = 0; i < nFloor; i++)
        {
            currentFloor = CreateEmptyGameObject("Floor" + i);
            currentFloor.transform.SetParent(map.transform);
            transform.position = new Vector3(0f, 10f * i, 0f);
            CreateFloor();
            platformIndexes.Add(platforms.Count - 1);
        }

        CreatePortals();

        //Brigdge stuff
        //Se due ponti sono di fila girane uno di 180°
        //Puoi modificare BridgeMoving in modo tale che quando collide cambia direzione

        foreach (CoupleGameobjectInt el in elementToAddOnMap)
            PlaceOnMap(el.prefab, el.quantity);

    }



    private void CreateFloor()
    {
        for (int i = 0; i < Dimension; i++)
            PerformAction();
    }

    private void PerformAction() {
        int action = iseed.Next(3);

        if (action == 0)
            history.Add(action, CreatePlatform());
        else if (action == 1)
        {
            history.Add(action);
            ChangeDirection();
        }
        else if (action == 2)
            history.Add(action, CreateBridge());
    }










    private void CreatePortals()
    {
        for (int i = 0; i < platformIndexes.Count - 1; i++)
        {
            int index = platformIndexes[i];

            GameObject portalObject1 = Instantiate(portalPrefab, platforms[index].transform.position + Vector3.up, Quaternion.identity);
            TeleportPortal portal1 = portalObject1.GetComponent<TeleportPortal>();

            GameObject portalObject2 = Instantiate(portalPrefab, platforms[index + 1].transform.position + Vector3.up, Quaternion.identity);
            TeleportPortal portal2 = portalObject2.GetComponent<TeleportPortal>();

            portal1.otherPortal = portal2;
            portal2.otherPortal = portal1;
        }
    }

    private GameObject CreateBridge()
    {
        Vector3 prev = transform.position;
        int distance = bridgeMinimumLength + iseed.Next(brigdeLength);
        transform.position += transform.forward * distance;
        Vector3 curr = transform.position;
        GameObject bridgeType = BrigdePrefab[iseed.Next(BrigdePrefab.Length)];
        GameObject bridge = Create(bridgeType, transform.position, transform.rotation);
        bridge.GetComponent<Bridge>().SetEndPoints(prev, curr);

        return bridge;
    }

    private GameObject CreatePlatform() {
        bool overlap = false;
        foreach (GameObject platf in platforms)
            if (platf.transform.position == transform.position)
                overlap = true;

        GameObject plat = null;
        if (!overlap) {
            int platformIndex = iseed.Next(100) < 80 ? 0 : 1;
            plat = Create(platformPrefabs[platformIndex], transform.position, transform.rotation);
            int newdim = platformsSize[iseed.Next(platformsSize.Length - 1)];
            plat.transform.localScale = new Vector3(newdim, plat.transform.localScale.y, newdim);
            platforms.Add(plat);
        }

        return plat;
    }

    private void ChangeDirection()
    {
        int newdirection = iseed.Next(4);
        transform.Rotate(0f, newdirection * 90f, 0f);
    }

    private GameObject Create(GameObject obj, Vector3 pos, Quaternion rot) {
        return Instantiate(obj, pos, rot, currentFloor.transform);
    }

    private GameObject CreateEmptyGameObject(string name) {
        GameObject temp = new GameObject();
        GameObject result = Instantiate(temp);
        result.name = name;
        Destroy(temp);
        return result;
    }

    private void PlaceOnMap(GameObject objPrefab, int quantity) {
        for (int i = 0; i < quantity; i++) {
            GameObject plat = platforms[iseed.Next(platforms.Count)];
            Vector3 platPos = plat.transform.position;
            int platDim = Mathf.FloorToInt(plat.transform.localScale.x);
            Vector3 pos = new Vector3(platPos.x + iseed.Next(platDim) - platDim / 2, platPos.y + 1, platPos.z + iseed.Next(platDim) - platDim / 2);
            Create(objPrefab, pos, Quaternion.identity);
        }
    }

    [Serializable]
    public struct CoupleGameobjectInt {
        public GameObject prefab;
        public int quantity;
    }

}
