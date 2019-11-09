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

    public Collider controlCollider;

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
    void Awake()
    {
        controls = new PlayerControls();

        controls.GamePlay.Jump.performed += Jump;

    }

    public void Start()
    {
        foreach(Collider col in gameObject.GetComponentsInChildren<Collider>())
        {
            col.enabled = false;

        }
        controlCollider.enabled = true;
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

            if (rb.velocity.y < 0.01f || rb.velocity.y > 0.01)
            {

                isGrounded = false;
                foreach (Transform groundCheck in groundChecks)
                {
                    groundRay = new Ray(groundCheck.position, groundCheck.forward);

                    if (Physics.Raycast(groundRay, rayLength))
                    {
                        isGrounded = true;
                        secondaryJump = false;
                        break;
                    }

                }

            }
            animator.SetBool("isJumping", !isGrounded);

            if (rb.velocity.y < 0 && !isHanging)
            {
                Ray ledgeRay = new Ray(ledgeCheck.transform.position, ledgeCheck.forward);
                if (Physics.Raycast(ledgeRay, rayLength*1.5f))
                {
                    rb.velocity = Vector3.zero;
                    rb.useGravity = false;
                    isHanging = true;
                    animator.SetBool("isHanging", isHanging);


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
            secondaryJump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * secondaryJumpForce, ForceMode.Impulse);
        }
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
            Debug.DrawLine(ledgeCheck.position, ledgeCheck.position + ledgeCheck.forward * rayLength * 1.5f, Color.green);
        }
        else
        {
            Debug.DrawLine(ledgeCheck.position, ledgeCheck.position + ledgeCheck.forward * rayLength * 1.5f, Color.red);
        }


    }
}
