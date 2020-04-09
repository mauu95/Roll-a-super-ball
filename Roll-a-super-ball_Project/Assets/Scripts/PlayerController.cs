using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public Text countText;
    public Text winText;
    public Material bounceMaterial;

    private Rigidbody rb;
    private int count;
    private bool isGrounded;
    private bool canJump;


    private void Start()
    {        rb = GetComponent<Rigidbody>();
        count = 0;
        countText.text = "COUNT: " + count.ToString();
    }

    private void FixedUpdate()
    {
        if (canJump && isGrounded && Input.GetKey(KeyCode.Space))
        {
            isGrounded = false;
            rb.AddForce(0f, 50f, 0f, ForceMode.Impulse);
        }


        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        rb.AddForce(movement * speed, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            countText.text = "COUNT: " + count.ToString();

            if (count == 4)
                winText.gameObject.SetActive(true);


            MeshRenderer mr = GetComponent<MeshRenderer>();
            mr.material = bounceMaterial;
            canJump = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

}
