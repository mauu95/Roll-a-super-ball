using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PCGMap : MonoBehaviour {
    public int seed;
    public int Dimension = 20;
    public int nFloor = 1;
    public int nPickUps;
    public GameObject platformPrefab;
    public GameObject BrigdePrefab;
    public CoupleGameobjectInt[] elementToAddOnMap;
    public GameObject portalPrefab;


    private IteratorSeed iseed;
    private List<GameObject> platforms;
    private GameObject map;
    private int[] platformsSize = new int[] { 5, 10, 15, 20 };

    private GameObject currentFloor;

    private void Start() {
        CreateMap();
    }

    private void CreateMap() {
        iseed = new IteratorSeed(seed);
        platforms = new List<GameObject>();
        map = CreateEmptyGameObject("Map");



        List<int> platformIndexes = new List<int>();
        for (int i = 0; i < nFloor; i++) {
            currentFloor = CreateEmptyGameObject("Floor" + i);
            currentFloor.transform.SetParent(map.transform);
            transform.position = new Vector3(0f, 10f * i, 0f);
            CreateFloor();
            platformIndexes.Add(platforms.Count - 1);
        }

        // ignore last index
        for (int i = 0; i < platformIndexes.Count - 1; i++) {
            int index = platformIndexes[i];

            GameObject portalObject1 = Instantiate(portalPrefab, platforms[index].transform.position + Vector3.up, Quaternion.identity);
            TeleportPortal portal1 = portalObject1.GetComponent<TeleportPortal>();

            GameObject portalObject2 = Instantiate(portalPrefab, platforms[index + 1].transform.position + Vector3.up, Quaternion.identity);
            TeleportPortal portal2 = portalObject2.GetComponent<TeleportPortal>();

            portal1.otherPortal = portal2;
            portal2.otherPortal = portal1;
        }

        foreach(CoupleGameobjectInt el in elementToAddOnMap)
            PlaceOnMap(el.prefab, el.quantity);

    }

    private void CreateFloor() {
        for (int i = 0; i < Dimension; i++)
            PerformAction();
    }

    private void PerformAction() {
        int action = iseed.Next(3);

        if (action == 0)
        { // Place a platform
            CreatePlatform();
        }
        else if (action == 1) { // Change direction
            int newdirection = iseed.Next(4);
            transform.Rotate(0f, newdirection * 90f, 0f);
        } else if (action == 2) { // Place a bridge
            Vector3 prev = transform.position;
            int distance = 5 + iseed.Next(2) * 10 + iseed.Next(9);

            transform.position += transform.forward * distance;
            Vector3 curr = transform.position;

            Vector3 pos = (curr - prev) / 2 + prev;
            GameObject temp = Create(BrigdePrefab, pos, transform.rotation);

            Vector3 scale = temp.transform.localScale;
            temp.transform.localScale = new Vector3(scale.x, scale.y, scale.z * distance);
        }
    }

    private void CreatePlatform()
    {
        bool overlap = false;
        foreach (GameObject plat in platforms)
            if (plat.transform.position == transform.position)
                overlap = true;

        if (!overlap)
        {
            GameObject temp = Create(platformPrefab, transform.position, transform.rotation);
            int newdim = platformsSize[iseed.Next(platformsSize.Length - 1)];
            temp.transform.localScale = new Vector3(newdim, temp.transform.localScale.y, newdim);
            platforms.Add(temp);
        }
    }

    private GameObject Create(GameObject obj, Vector3 pos, Quaternion rot) {
        return Instantiate(obj, pos, rot, currentFloor.transform);
    }

    private GameObject CreateEmptyGameObject(string name)
    {
        GameObject temp = new GameObject();
        GameObject result = Instantiate(temp);
        result.name = name;
        Destroy(temp);
        return result;
    }

    private void PlaceOnMap(GameObject objPrefab, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject plat = platforms[iseed.Next(platforms.Count)];
            Vector3 platPos = plat.transform.position;
            int platDim = Mathf.FloorToInt(plat.transform.localScale.x);
            Vector3 pos = new Vector3(platPos.x + iseed.Next(platDim) - platDim/2, platPos.y + 1, platPos.z + iseed.Next(platDim) - platDim/2);
            Create(objPrefab, pos, Quaternion.identity);
        }
    }

    [Serializable]
    public struct CoupleGameobjectInt
    {
        public GameObject prefab;
        public int quantity;
    }

}
