using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvalPlayerMalus : MonoBehaviour {
    // Start is called before the first frame update
    public Vector3 size = new Vector3(1, 1.5f, 0.5f);
    public float duration = 20;

    private float elapsedTime = 0f;
    private Vector3 initialScale;
    void Start() {
        initialScale = transform.localScale;
        transform.localScale = new Vector3(1, 1.5f, 0.5f);
    }

    // Update is called once per frame
    void Update() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= duration) {
            transform.localScale = initialScale;
            Destroy(this);
        }

    }
}
