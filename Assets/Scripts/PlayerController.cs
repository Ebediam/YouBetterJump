using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float acceleration;
    public float maxSpeed;
    public float jumpForce;
    public List<Transform> groundChecks;
    public Transform ledgeCheck;

    public bool isGrounded = true;
    public bool isHanging = false;
    public Ray groundRay;
    public float rayLength;
    public bool disableGroundCheck = false;
    public bool secondaryJump = false;
    public float secondaryJumpForce;

    public float noGroundCheckTimer;

    float groundTimer;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!disableGroundCheck)
        {
            if (Mathf.Abs(rb.velocity.y) < 0.1f)
            {
                isGrounded = false;
                foreach (Transform groundCheck in groundChecks)
                {
                    groundRay = new Ray(groundCheck.position, groundCheck.forward);

                    if (Physics.Raycast(groundRay, rayLength))
                    {
                        Debug.DrawLine(groundCheck.position, groundCheck.position + groundCheck.forward * rayLength, Color.green);
                        isGrounded = true;
                        secondaryJump = false;
                        break;
                    }
                    else
                    {
                        Debug.DrawLine(groundCheck.position, groundCheck.position + groundCheck.forward * rayLength, Color.red);
                    }
                }
            }
            

            if (rb.velocity.y < 0 && !isHanging)
            {
                Ray ledgeRay = new Ray(ledgeCheck.transform.position, ledgeCheck.forward);
                if (Physics.Raycast(ledgeRay, rayLength*2))
                {
                    rb.velocity = Vector3.zero;
                    rb.useGravity = false;
                    isHanging = true;


                }
            }



        }



        if (!isHanging)
        {
            rb.AddForce(transform.forward * acceleration, ForceMode.Force);
        }

        if (rb.velocity.z > maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxSpeed);
        }



        if (Input.GetAxis("Jump")> 0)
        {
            if (isGrounded || isHanging)
            {
                rb.useGravity = true;                
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                disableGroundCheck = true;
                isHanging = false;
            }
            else if(secondaryJump)
            {
                secondaryJump = false;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(transform.up * secondaryJumpForce, ForceMode.Impulse);
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

        animator.SetFloat("speedPercent", rb.velocity.z / maxSpeed);
        animator.SetBool("isHanging", isHanging);
        animator.SetBool("isJumping", !isGrounded);
        animator.SetFloat("verticalVelocity", rb.velocity.y);


    }
    public void ResetJump()
    {


    }
}
