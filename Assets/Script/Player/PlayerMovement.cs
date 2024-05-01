using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animatorPlayer;

    public float speed = 10.0f;
    public float jumpForce = 10.0f;
    public float gravityModifier = 1.0f;

    private Rigidbody rb;
    Vector3 movement;
 public float rotationSpeed = 100.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

  void Update()
    {
 float mouseX = Input.GetAxis("Mouse X");

        Vector3 rotation = new Vector3(0, mouseX * rotationSpeed, 0);

        transform.Rotate(rotation);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
         animatorPlayer.SetFloat("VelocityX", moveHorizontal);
        animatorPlayer.SetFloat("VelocityY", moveVertical);
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (Input.GetButtonDown("Jump") )
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
    // }    private void FixedUpdate()
    // {
    //     float moveHorizontal = Input.GetAxis("Horizontal");
    //     float moveVertical = Input.GetAxis("Vertical");

       
    //     rb.AddForce(movement * speed, ForceMode.Acceleration);
    //     rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

    //     if (rb.velocity.y < 0)
    //     {
    //         rb.velocity += Vector3.up * Physics.gravity.y * (gravityModifier - 1) * Time.deltaTime;
    //     }
    //     else if (rb.velocity.y > 0 && Input.GetButtonDown("Jump"))
    //     {
    //         rb.velocity += Vector3.up * jumpForce;
    //     }



    }
}
// public class PlayerRotation : MonoBehaviour
// {
//     public float rotationSpeed = 100.0f;
//     public float minRotation = -90.0f;
//     public float maxRotation = 90.0f;

//     void Update()
//     {
//         float mouseX = Input.GetAxis("Mouse X");

//         Vector3 rotation = new Vector3(0, mouseX * rotationSpeed, 0);

//         transform.Rotate(rotation);

//         float clampedRotation = Mathf.Clamp(transform.eulerAngles.y, minRotation, maxRotation);
//         transform.eulerAngles = new Vector3(0, clampedRotation, 0);
//     }
// }