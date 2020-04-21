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
        if( transform.position == target)
        {
            Vector3 temp = target;
            target = start;
            start = temp;
        }
        if(target != null)
            transform.position = moviment(target);

        if (Input.GetKeyDown(KeyCode.J))
        {
            SetEndPoints(Vector3.zero, new Vector3(10f, 0f));
        }
    }

    private Vector3 moviment( Vector3 pos ){
        return Vector3.MoveTowards( transform.position , pos , Time.deltaTime * speed );
    }

    public override void SetEndPoints(Vector3 start, Vector3 end)
    {
        transform.position = start;
        this.start = start;
        this.target = end;
    }
}
