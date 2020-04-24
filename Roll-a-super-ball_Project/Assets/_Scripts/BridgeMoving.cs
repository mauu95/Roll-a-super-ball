using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMoving : Bridge
{
    public float speed = 2;
    private Vector3 target;
    private Vector3 start;

    private void Start()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.up, 1F);

        foreach(RaycastHit hit in hits)
            if (hit.transform.GetComponent<BridgeNormal>())
                hit.transform.gameObject.SetActive(false);
    }

    void Update()
    {
        if( transform.position == target )
            SwapDirection();

        transform.position = moviment(target);
    }

    private void SwapDirection()
    {
        Vector3 temp = target;

        target = start;
        start = temp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name != "Player")
            SwapDirection();
    }

    private Vector3 moviment( Vector3 pos ){
        return Vector3.MoveTowards( transform.position , pos , Time.deltaTime * speed );
    }

    public override void SetEndPoints(Vector3 start, Vector3 end)
    {
        transform.position = (end - start) / 2 + start;
        this.start = start;
        this.target = end;
    }
}
