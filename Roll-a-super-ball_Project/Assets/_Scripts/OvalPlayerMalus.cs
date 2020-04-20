using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvalPlayerMalus : MonoBehaviour {
    // Start is called before the first frame update
    public Vector3 size = new Vector3(1, 1.5f, 0.5f);
    public float velocityNeeded = 3;
    public float duration = 20;

    private float elapsedTime = 0f;
    private Vector3 initialScale;
    private void Start() {
        elapsedTime = duration;
        initialScale = transform.localScale;
    }
    // Update is called once per frame
    void Update() {
        if (elapsedTime >= duration) {
            transform.localScale = initialScale;
        } else {
            elapsedTime += Time.deltaTime;
        }
    }

    public void Activate() {
        if (GetComponent<Rigidbody>().velocity.magnitude > velocityNeeded) {
            elapsedTime = 0;
            transform.localScale = size;
        }
    }
}
