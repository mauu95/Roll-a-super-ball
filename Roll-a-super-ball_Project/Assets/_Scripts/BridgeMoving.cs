using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMoving : Bridge
{
    public float speed = 2;
    private Vector3 target;
    private Vector3 start;
    private bool stop;

    private void Start()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 1f, transform.forward);

        foreach (RaycastHit hit in hits)
            if (hit.transform.GetComponent<BridgeNormal>())
                hit.transform.gameObject.SetActive(false);

        float temp = speed;
        speed = 1000f;
        StartCoroutine(SetSpeed(temp));

    }

    IEnumerator SetSpeed(float s)
    {
        yield return new WaitForSeconds(0.2f);
        this.speed = s;
    }

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

        yield return new WaitForSeconds(0.2f);
        stop = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name != "Player")
            StartCoroutine(SwapDirection());
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
