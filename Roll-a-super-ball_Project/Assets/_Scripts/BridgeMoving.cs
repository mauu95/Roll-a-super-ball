using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMoving : Bridge
{
    public float speed = 2;
    private Vector3 target;
    private Vector3 start;
    private bool stop;

    void Update()
    {
        if( transform.position == target )
            StartCoroutine(SwapDirection());

        if(!stop)
            transform.position = moviment(target);
    }

    IEnumerator SwapDirection()
    {
        Vector3 temp = target;
        target = start;
        start = temp;
        stop = true;

        yield return new WaitForSeconds(1f);
        stop = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name != "Player")
            StartCoroutine(SwapDirection());
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject p = collision.gameObject;
        if (p.name != "Player")
            return;

        p.GetComponent<Rigidbody>().AddForce((target-start).normalized *100);

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
