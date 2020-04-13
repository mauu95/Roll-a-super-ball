﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float speed;
    [Range(0f, 1f)]
    public float controlInAir;
    public float maxSpeed = 30f;
    public Text countText;
    public Text winText;
    public Transform MainCamera;

    [HideInInspector]
    public bool isGrounded;

    [HideInInspector]
    public Vector3 movement;

    private Rigidbody rb;
    private int count;


    private void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        countText.text = "COUNT: " + count.ToString();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
            force *= controlInAir;

        rb.AddForce(force, ForceMode.VelocityChange);
    }

    public Vector3 GetForwardDirection() {
        return Vector3.Normalize(MainCamera.position - transform.position) * -10;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count++;
            countText.text = "COUNT: " + count.ToString();

            if (count == 4)
                winText.gameObject.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Ground") 
            isGrounded = true;
    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Ground") 
            isGrounded = false;
    }

    public void AlterSpeeds(float value) {
        controlInAir *= value;
        maxSpeed *= value;
        speed *= value;
    }

}
