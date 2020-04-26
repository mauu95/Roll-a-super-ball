using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private void Start()
    {
        //StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            CreateEnemy();
    }

    public void CreateEnemy()
    {
        Instantiate(PrefabManager.instance.enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>().SetPlatform(gameObject);
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(0.2f);
        CreateEnemy();
    }
}
