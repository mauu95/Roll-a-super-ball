using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public void CreateEnemy()
    {
        Instantiate(PrefabManager.instance.enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>().SetPlatform(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            CreateEnemy();
    }
}
