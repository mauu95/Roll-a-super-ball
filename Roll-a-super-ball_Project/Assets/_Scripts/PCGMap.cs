using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PCGMap : MonoBehaviour {
    public int seed;
    public int Dimension = 20;
    public int nFloor = 1;
    public int brigdeLength = 25;
    public int nPickUps;
    public int nAgents;
    public GameObject agentPrefab;
    public GameObject[] platformPrefabs;
    public GameObject[] BrigdePrefab;
    public GameObject movingBrigdePrefab;
    public GameObject bridgeDestroyerPrefab;
    public CoupleGameobjectInt[] elementToAddOnMap;
    public GameObject portalPrefab;
    [HideInInspector]
    public PCGHistory history;

    private NavMeshSurface surface;
    private IteratorSeed iseed;
    private List<GameObject> platforms;
    private List<int> platformIndexes;
    private GameObject map;
    private int[] platformsSize = new int[] { 16, 20, 24 };
    private int bridgeMinimumLength = 5;
    private GameObject normalPlatformPrefab;
    private GameObject currentFloor;

    private void Start() {
        if (elementToAddOnMap.Length > 0)
            if (elementToAddOnMap[0].prefab.name == "PickUp") elementToAddOnMap[0].quantity = GameManager.instance.nPickUp;
        foreach (GameObject obj in platformPrefabs) {
            if (!obj.name.StartsWith("HoledPlatform", StringComparison.Ordinal)) {
                normalPlatformPrefab = obj;
            }
        }
        platformIndexes = new List<int>();
        history = new PCGHistory();
        surface = FindObjectOfType<NavMeshSurface>();
        CreateMap();
        StartCoroutine(BuildNavMesh());
    }

    private void CreateMap() {
        iseed = new IteratorSeed(seed);
        platforms = new List<GameObject>();
        map = CreateEmptyGameObject("Map");

        for (int i = 0; i < nFloor; i++) {
            currentFloor = CreateEmptyGameObject("Floor" + i);
            currentFloor.transform.SetParent(map.transform);
            transform.position = new Vector3(0f, 20f * i, 0f);
            CreateFloor();
            platformIndexes.Add(platforms.Count - 1);
        }

        CreatePortals();
        CreateMovingBridges();
        CreateAgents();

        foreach (CoupleGameobjectInt el in elementToAddOnMap)
            PlaceOnMap(el.prefab, el.quantity);

    }

    private void CreateAgents()
    {
        for(int i = 0; i < nAgents; i++)
        {
            GameObject plat = platforms[iseed.Next(platforms.Count)];
            Vector3 platPos = plat.transform.position;
            int platDim = Mathf.FloorToInt(plat.transform.localScale.x);
            Vector3 pos = new Vector3(platPos.x + iseed.Next(platDim) - platDim / 2, platPos.y - 2, platPos.z + iseed.Next(platDim) - platDim / 2);
            GameObject agent = Instantiate(agentPrefab, pos, Quaternion.identity);
            agent.GetComponent<Enemy>().SetPlatform(plat);
        }
    }

    private void CreateFloor() {
        for (int i = 0; i < Dimension; i++)
            PerformAction();
    }

    private void PerformAction() {
        int action = iseed.Next(3);

        if (action == 0) {
            GameObject plat = CreatePlatform();
            if (plat)
                history.Add(action, plat);
        } else if (action == 1) {
            history.Add(action);
            ChangeDirection();
        } else if (action == 2)
            history.Add(action, CreateBridge());
    }










    IEnumerator BuildNavMesh() {
        yield return new WaitForSeconds(0.2f);
        if (surface)
            surface.BuildNavMesh();
    }

    private void ChangeDirection() {
        int newdirection = iseed.Next(4);
        transform.Rotate(0f, newdirection * 90f, 0f);
    }

    private void CreateMovingBridges() {
        PCGHistory.SearchPatternResult[] bridges = history.SearchBrigde("01*2+1*0");

        for (int i = 0; i < bridges.Length; i++) {
            PCGHistory.SearchPatternResult curr = bridges[i];

            GameObject platform1 = history.GetElement(curr.index).obj;
            float scale1 = platform1.transform.localScale.x;

            GameObject platform2 = history.GetElement(curr.index + curr.match.Length - 1).obj;
            float scale2 = platform2.transform.localScale.x;

            float distance = (platform2.transform.position - platform1.transform.position).magnitude - (scale1 + scale2) / 2;

            if (distance > 5) {
                Vector3 startPoint = history.GetElement(curr.index).obj.transform.position;
                Vector3 endPoint = history.GetElement(curr.index + curr.match.Length - 1).obj.transform.position;

                if (Math.Abs(startPoint.y - endPoint.y) > Mathf.Epsilon)
                    return;

                GameObject bridgeDestroyer = Instantiate(bridgeDestroyerPrefab, platform1.transform.parent);
                bridgeDestroyer.transform.rotation = history.GetElement(curr.indexOfBridge).obj.transform.rotation;
                bridgeDestroyer.GetComponent<BridgeDestroyer>().SetEndPoints(startPoint, endPoint);

                CreateMovingBridge(curr, platform1, startPoint, endPoint);

                if (distance > 20)
                    CreateMovingBridge(curr, platform1, endPoint, startPoint);
            }
        }
    }

    private GameObject CreateMovingBridge(PCGHistory.SearchPatternResult curr, GameObject parent, Vector3 startPoint, Vector3 endPoint) {
        GameObject movingBridge = Instantiate(movingBrigdePrefab, parent.transform.parent);
        movingBridge.transform.rotation = history.GetElement(curr.indexOfBridge).obj.transform.rotation;
        movingBridge.transform.position = (startPoint + endPoint) / 2 + startPoint;
        movingBridge.GetComponent<BridgeMoving>().SetEndPoints(startPoint, endPoint);
        return movingBridge;
    }

    private void CreatePortals() {
        for (int i = 0; i < platformIndexes.Count - 1; i++) {
            int index = platformIndexes[i];
            platforms[index] = ReplaceHoledPlatformIfNeeded(platforms[index]);
            GameObject portalObject1 = Instantiate(portalPrefab, platforms[index].transform.position + Vector3.up * 0.2f, portalPrefab.transform.rotation, platforms[index].transform.parent);
            Teleportal portal1 = portalObject1.GetComponent<Teleportal>();

            platforms[index + 1] = ReplaceHoledPlatformIfNeeded(platforms[index + 1]);
            GameObject portalObject2 = Instantiate(portalPrefab, platforms[index + 1].transform.position + Vector3.up * 0.2f, portalPrefab.transform.rotation, platforms[index + 1].transform.parent);
            Teleportal portal2 = portalObject2.GetComponent<Teleportal>();

            portal1.otherPortal = portal2;
            portal2.otherPortal = portal1;
            portal2.IsGoingDown();

        }
    }

    private GameObject ReplaceHoledPlatformIfNeeded(GameObject platform) {
        if (platform.name.StartsWith("HoledPlatform", StringComparison.Ordinal)) {
            GameObject newPlatform = Create(normalPlatformPrefab, platform.transform.position, platform.transform.rotation);
            Vector3 scale = platform.transform.localScale;
            scale.y = 0.1f;
            newPlatform.transform.localScale = scale;
            Destroy(platform);
            return newPlatform;
        }
        return platform;
    }

    private GameObject CreateBridge() {
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
            int platformIndex = 0;
            if (platformPrefabs.Length > 1)
                platformIndex = iseed.Next(100) < 80 ? 0 : 1;

            plat = Create(platformPrefabs[platformIndex], transform.position, transform.rotation);
            int newdim = platformsSize[iseed.Next(platformsSize.Length - 1)];
            plat.transform.localScale = new Vector3(newdim, plat.transform.localScale.y, newdim);
            platforms.Add(plat);
        }

        return plat;
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
