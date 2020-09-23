using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RobotMovementV2 : MonoBehaviour
{

    public float jumpForce = 8f;
    public float movementSpeed = 200f;
    public float afkTimer = 10.0f;

    private float startTime;
    private float InitialAfkTimer;
    private bool moved = false;
    private bool isDancing = false;

    private bool canJump = false;
    private bool jump = false;
    private Animator animator;

    private void Start()
    {
        InitialAfkTimer = afkTimer;
        animator = GetComponent<Animator>();
        animator.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {

        transform.position += transform.forward * Time.deltaTime * movementSpeed / 50 * Input.GetAxis("Vertical");
        transform.position += transform.right * Time.deltaTime * movementSpeed / 50 * Input.GetAxis("Horizontal");

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            moved = false; //No movement

            animator.SetBool("isWalking", false);
        }
        else
        {
            if (!moved)
            {
                animator.SetBool("isWalking", true);
                ResetPosition();
            }

        }


        if (Input.GetButtonDown("Jump") && canJump && !jump)
        {
            jump = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jump = false;
        }


        afkTimer -= Time.deltaTime;
        if (afkTimer < 0 && !isDancing)
        {
            isDancing = true;
            animator.SetBool("isDancing", true);
        }

    }
    void ResetPosition()
    {
        startTime = Time.time;
        animator.SetBool("isDancing", false);
        moved = true;
        afkTimer = InitialAfkTimer;
        isDancing = false;
    }
    void FixedUpdate()
    {
        if (jump && canJump)
        {
            canJump = false;
            GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }
    private void OnDisable()
    {
        animator.SetBool("isWalking", false);
        ResetPosition();
    }
    void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }
}
