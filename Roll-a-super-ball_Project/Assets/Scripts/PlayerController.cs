using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float speedInAir;
    public Text countText;
    public Text winText;
    public Material bounceMaterial;
    public Transform MainCamera;
    public float JumpForce;

    private Rigidbody rb;
    private int count;
    private bool isGrounded;
    private bool canJump;


    private void Start()
    {        
        rb = GetComponent<Rigidbody>();
        count = 0;
        countText.text = "COUNT: " + count.ToString();
    }

    private void FixedUpdate()
    {
        if (canJump && isGrounded && Input.GetKey(KeyCode.Space))
        {
            isGrounded = false;
            rb.AddForce(0f, JumpForce, 0f, ForceMode.Impulse);
        }

            Vector3 forward = Vector3.Normalize(MainCamera.position - transform.position) * -10;
            Vector3 lateral = Vector3.Cross(forward, Vector3.up).normalized * -10;
            Vector3 moveHorizontal = Input.GetAxis("Horizontal") * lateral;
            Vector3 moveVertical = Input.GetAxis("Vertical") * forward;
            Vector3 temp = moveHorizontal + moveVertical;

            Vector3 movement = new Vector3(temp.x, 0f, temp.z);
            if(isGrounded)
                rb.AddForce(movement * speed, ForceMode.VelocityChange);
            else
                rb.AddForce(movement * speedInAir , ForceMode.VelocityChange);
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
