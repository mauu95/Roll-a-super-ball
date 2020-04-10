using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float speedInAir;
    public Text countText;
    public Text winText;
    public Transform MainCamera;
    public bool isGrounded;

    private Rigidbody rb;
    private int count;


    private void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        countText.text = "COUNT: " + count.ToString();
    }

    private void FixedUpdate() {
        Vector3 forward = Vector3.Normalize(MainCamera.position - transform.position) * -10;
        Vector3 lateral = Vector3.Cross(forward, Vector3.up).normalized * -10;
        Vector3 moveHorizontal = Input.GetAxis("Horizontal") * lateral;
        Vector3 moveVertical = Input.GetAxis("Vertical") * forward;
        Vector3 temp = moveHorizontal + moveVertical;

        Vector3 movement = new Vector3(temp.x, 0f, temp.z);
        if (isGrounded)
            rb.AddForce(movement * speed, ForceMode.VelocityChange);
        else
            rb.AddForce(movement * speedInAir, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count++;
            countText.text = "COUNT: " + count.ToString();

            if (count == 4)
                winText.gameObject.SetActive(true);

            gameObject.AddComponent<PowerUpJump>();
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }

}
