using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMoving : Bridge
{
    public float speed = 2;
    private Vector3 target;
    private Vector3 start;

    void Update()
    {
        if( transform.position == target )
            SwapDirection();

        if (target != null)
            transform.position = moviment(target);

        if (Input.GetKeyDown(KeyCode.J))
        {
            SetEndPoints(new Vector3(0f, 0f, -5f), new Vector3(0f, 0f, 5f));
        }
    }

    private void SwapDirection()
    {
        Vector3 temp = target;
        target = start;
        start = temp;
    }

    private void OnCollisionEnter(Collision collision)
    {
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
