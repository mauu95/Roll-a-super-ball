using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMovement : MonoBehaviour
{
    public int distance = 2;
    public float speed = 2;
    public bool horizontal = true;
    private int flag = 0;
    private Vector3 targhet;
    private Vector3 start;

    void Start() {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        start = transform.position;
        if( horizontal ) 
            targhet = new Vector3( x + distance , y , z );    
        else
            targhet = new Vector3( x , y + distance , z );
    }
    void Update()
    {
            setFlag( transform.position );
            if( flag == 0 ){
                 transform.position = moviment( targhet );
            } else if ( flag == 1 ){
                transform.position = moviment( start );
            }
    }

    private void setFlag( Vector3 position ){
        if( position.Equals( targhet ) )
            flag = 1;
        else if( position.Equals( start ))
            flag = 0;
    }

    private Vector3 moviment( Vector3 pos ){
        return Vector3.MoveTowards( transform.position , pos , Time.deltaTime * speed );
    }
}
