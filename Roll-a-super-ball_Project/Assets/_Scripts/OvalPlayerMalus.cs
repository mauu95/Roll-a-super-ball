using UnityEngine;

public class OvalPlayerMalus : MonoBehaviour {
    public Vector3 size = new Vector3(1, 1.5f, 0.5f);
    public float duration = 20;
    public float crashThreshold = 15.0f;

    private Vector3 initialScale;
    private Rigidbody rb;
    private float oldVelocity;
    private float returnNormalAt;

    private void Start() {
        initialScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
        oldVelocity = rb.velocity.y;
        crashThreshold *= crashThreshold;
    }

    // Update is called once per frame
    void Update() {
        if (Time.realtimeSinceStartup >= returnNormalAt) {
            transform.localScale = initialScale;
        }
    }

    void FixedUpdate() {
        if (oldVelocity - rb.velocity.sqrMagnitude > crashThreshold) {
            Activate();
        }
        oldVelocity = rb.velocity.sqrMagnitude;
    }

    public void Activate() {
        returnNormalAt = Time.realtimeSinceStartup + duration;
        transform.localScale = size;
    }
}
