using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float speed = 5;
    [Range(0f, 1f)]
    public float playerControlInAir = 0.5f;
    public float maxSpeed = 30f;
    public bool isGrounded;

    public Transform MainCamera;
    [HideInInspector]
    public Vector3 movement;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.position.y < -5f)
            GameManager.instance.RealoadLevel();
    }

    private void FixedUpdate() {
        Vector3 forward = GetForwardDirection();
        Vector3 lateral = Vector3.Cross(forward, Vector3.up).normalized * -10;
        Vector3 moveHorizontal = Input.GetAxis("Horizontal") * lateral;
        Vector3 moveVertical = Input.GetAxis("Vertical") * forward;
        Vector3 temp = moveHorizontal + moveVertical;

        movement = new Vector3(temp.x, 0f, temp.z);

        Vector3 force = movement * speed / 100;
        if (!isGrounded)
            force *= playerControlInAir;

        rb.AddForce(force, ForceMode.VelocityChange);
    }

    public Vector3 GetForwardDirection() {
        return Vector3.Normalize(MainCamera.position - transform.position) * -10;
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == "Ground")
            isGrounded = true;
    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Ground")
            isGrounded = false;
    }

    public void AlterSpeeds(float value) {
        playerControlInAir *= value;
        maxSpeed *= value;
        speed *= value;
    }

    public void SetVelocity(Vector3 vel) {
        rb.velocity = vel;
    }

}
