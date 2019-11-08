using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float maxSpeed;
    public float jumpForce;
    public List<Transform> groundChecks;
    public bool isGrounded = true;
    public Ray groundRay;
    public float rayLength;
    public bool disableGroundCheck = false;

    public float noGroundCheckTimer;

    float groundTimer;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!disableGroundCheck)
        {
            isGrounded = false; 
            foreach (Transform groundCheck in groundChecks)
            {
                groundRay = new Ray(groundCheck.position, groundCheck.forward);

                if (Physics.Raycast(groundRay, rayLength))
                {
                    Debug.DrawLine(groundCheck.position, groundCheck.position + groundCheck.forward * rayLength, Color.green);
                    isGrounded = true;
                    break;
                }
                else
                {
                    Debug.DrawLine(groundCheck.position, groundCheck.position + groundCheck.forward * rayLength, Color.red);
                }
            }
        }
        


        rb.AddForce(transform.forward * speed, ForceMode.Force);

        if(rb.velocity.z > maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxSpeed);
        }

        if (Input.GetAxis("Jump")> 0)
        {
            if (isGrounded)
            {   

                Debug.Log("JUMP!");
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                disableGroundCheck = true;
            }
            else
            {
                Debug.Log("Key pressed but not on the ground");
            }
        }

    }

    public void Update()
    {
        if (disableGroundCheck)
        {
            groundTimer += Time.deltaTime;
            if(groundTimer >= noGroundCheckTimer)
            {
                disableGroundCheck = false;
                groundTimer = 0f;
            }
        }


    }
    public void ResetJump()
    {


    }
}
