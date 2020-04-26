using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeDestroyer : Bridge
{

    private void Start()
    {
        StartCoroutine(Autodestruction());
    }

    IEnumerator Autodestruction()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    public override void SetEndPoints(Vector3 start, Vector3 end)
    {
        Vector3 pos = (end - start) / 2 + start;
        transform.position = pos;
        Vector3 scale = transform.localScale;
        float distance = (end - start).magnitude;
        transform.localScale = new Vector3(scale.x, scale.y, scale.z * distance - 10f);
    }

    private void OnTriggerStay(Collider collision)
    {
        GameObject other = collision.gameObject;

        if (other.GetComponent<BridgeNormal>())
            other.SetActive(false);
    }
}
