using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCreator : MonoBehaviour
{
    public GameObject menuPrefab;
    void Start()
    {
        Instantiate(menuPrefab, transform.parent);
        Destroy(gameObject);
    }
}
