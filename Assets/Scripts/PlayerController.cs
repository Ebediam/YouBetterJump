using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerControls controls;
    public Rigidbody rb;
    public float acceleration;
    public float maxSpeed;
    public float jumpForce;
    public List<Transform> groundChecks;
    public Transform ledgeCheck;
    public Transform ceilingCheck;

    public Collider controlCollider;

    public bool isGrounded = true;
    public bool isHanging = false;
    public Ray groundRay;
    public float rayLength;
    public bool disableGroundCheck = false;
    public bool secondaryJump = false;
    public float secondaryJumpForce;

    public bool isHovering = false;
    public float hoverTime;
    public float hoverForce;

    public float noGroundCheckTimer;

    float groundTimer;

    public Animator animator;

    public Material material;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();

        controls.GamePlay.Jump.performed += Jump;
        controls.GamePlay.Jump.canceled += CancelJump;
        

    }

    public void Start()
    {
        GameController.RestartEvent += Restart;
        GoalPost.SceneChangeEvent += SceneChange;

    }

    public void SceneChange()
    {
        GameController.RestartEvent -= Restart;
        GoalPost.SceneChangeEvent -= SceneChange;
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        DrawDebugLines();
        if (!disableGroundCheck)
        {
            if (rb.velocity.y < 0.1f || rb.velocity.y > 0.1)
            {

                isGrounded = false;
                foreach (Transform groundCheck in groundChecks)
                {
                    groundRay = new Ray(groundCheck.position, groundCheck.forward);

                    if (Physics.Raycast(groundRay, rayLength))
                    {
                        isGrounded = true;
                        isHovering = false;
                        DisableSecondaryJump();
                        break;
                    }

                }

            }
            animator.SetBool("isJumping", !isGrounded);

            if (rb.velocity.y < -0.1f && !isHanging && !isGrounded)
            {
                Ray ceilingRay = new Ray(ceilingCheck.transform.position, ceilingCheck.forward);
                if(!Physics.Raycast(ceilingRay, rayLength))
                {
                    Ray ledgeRay = new Ray(ledgeCheck.transform.position, ledgeCheck.forward);
                    if (Physics.Raycast(ledgeRay, rayLength ))
                    {
                        rb.velocity = Vector3.zero;
                        rb.useGravity = false;
                        isHanging = true;
                        animator.SetBool("isHanging", isHanging);


                    }
                }


            }



        }



        if (!isHanging)
        {
            rb.AddForce(transform.forward * acceleration, ForceMode.Force);
            if (isHovering)
            {
                rb.AddForce(transform.up * hoverForce, ForceMode.Force);
            }
        }

        if (rb.velocity.z > maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxSpeed);
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


        animator.SetFloat("verticalVelocity", rb.velocity.y);


    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("JUMP");
        if (isGrounded || isHanging)
        {
            rb.useGravity = true;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
            disableGroundCheck = true;
            isHanging = false;
            animator.SetBool("isHanging", isHanging);

        }
        else if (secondaryJump)
        {
            DisableSecondaryJump();
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * secondaryJumpForce, ForceMode.Impulse);
        }
        isHovering = true;
        Invoke("CancelJumpInmediate", hoverTime);

    }

    public void CancelJump(InputAction.CallbackContext context)
    {
        CancelInvoke("CancelJumpInmediate");
        CancelJumpInmediate();
    }

    public void CancelJumpInmediate()
    {
        CancelInvoke("CancelJumpInmediate");
        isHovering = false;
    }

    public void OnEnable()
    {
        controls.GamePlay.Enable();

    }

    public void OnDisable()
    {
        controls.GamePlay.Disable();
    }

    public void ActivateRagdoll()
    {
        foreach(Collider col in gameObject.GetComponentsInChildren<Collider>())
        {
            col.enabled = true;

        }
        controlCollider.enabled = false;
        
    }

    public void Restart()
    {
        transform.position = GameController.local.spawnPoint.position;
        rb.velocity = Vector3.zero;
        isGrounded = false;
    }

    public void EnableSecondaryJump()
    {
        secondaryJump = true;
        material.SetFloat("_hasDoubleJump", 1);
    }

    public void DisableSecondaryJump()
    {
        secondaryJump = false;
        material.SetFloat("_hasDoubleJump", 0);
    }

    public void DrawDebugLines()
    {
        foreach (Transform groundCheck in groundChecks)
        {
            groundRay = new Ray(groundCheck.position, groundCheck.forward);

            if (Physics.Raycast(groundRay, rayLength))
            {
                Debug.DrawLine(groundCheck.position, groundCheck.position + groundCheck.forward * rayLength, Color.green);
            }
            else
            {
                Debug.DrawLine(groundCheck.position, groundCheck.position + groundCheck.forward * rayLength, Color.red);
            }
        }

        Ray ledgeRay = new Ray(ledgeCheck.transform.position, ledgeCheck.forward);
        if (Physics.Raycast(ledgeRay, rayLength * 1.5f))
        {
            Debug.DrawLine(ledgeCheck.position, ledgeCheck.position + ledgeCheck.forward * rayLength, Color.green);
        }
        else
        {
            Debug.DrawLine(ledgeCheck.position, ledgeCheck.position + ledgeCheck.forward * rayLength, Color.red);
        }

        Ray ceilingRay = new Ray(ceilingCheck.transform.position, ceilingCheck.forward);
        if (Physics.Raycast(ceilingRay, rayLength))
        {
            Debug.DrawLine(ceilingCheck.position, ceilingCheck.position + ledgeCheck.forward * rayLength, Color.green);
        }
        else
        {
            Debug.DrawLine(ceilingCheck.position, ceilingCheck.position + ceilingCheck.forward * rayLength, Color.red);
        }

    }
}
