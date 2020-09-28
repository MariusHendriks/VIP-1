using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class RobotMovementV2 : MonoBehaviour
{

    public float jumpForce = 8f;
    public float movementSpeed = 200f;
    public float afkTimer = 10.0f;
    public float lowJumpMultiplier = 1.5f;
    public float fallMultiplier = 2f;

    public ParticleSystem JetpackSmokeLeft;
    public ParticleSystem JetpackSmokeRight;
    public ParticleSystem JetpackFireLeft;
    public ParticleSystem JetpackFireRight;
    public GameObject JetpackLightLeft;
    public GameObject JetpackLightRight;

    private float startTime;
    private float InitialAfkTimer;
    private bool moved = false;
    private bool isDancing = false;

    private bool jump = false;
    private Animator animator;
    private Rigidbody rigidBody;



    private void Start()
    {

        InitialAfkTimer = afkTimer;
        animator = GetComponent<Animator>();
        animator.enabled = true;
        rigidBody = GetComponent<Rigidbody>();
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


        if (Input.GetButtonDown("Jump") && !jump)
        {
            StartParticles();
            jump = true;
            ResetPosition();

        }
        if (Input.GetButtonUp("Jump"))
        {
            StopParticles();
            jump = false;
        }


        afkTimer -= Time.deltaTime;
        if (afkTimer < 0 && !isDancing)
        {
            isDancing = true;
            animator.SetBool("isDancing", true);
        }

        if (rigidBody.velocity.y < 0)
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
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
        if (jump)
        {
            GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }

    }
    private void OnDisable()
    {
        animator.SetBool("isWalking", false);
        ResetPosition();
    }
    private void StopParticles()
    {
        JetpackSmokeLeft.Stop();
        JetpackSmokeRight.Stop();
        JetpackFireLeft.Stop();
        JetpackFireRight.Stop();
        JetpackLightLeft.GetComponent<Light>().intensity = 0;
        JetpackLightRight.GetComponent<Light>().intensity = 0;
    }
    private void StartParticles()
    {
        JetpackSmokeLeft.Play();
        JetpackSmokeRight.Play();
        JetpackFireLeft.Play();
        JetpackFireRight.Play();
        JetpackLightLeft.GetComponent<Light>().intensity = 1;
        JetpackLightRight.GetComponent<Light>().intensity = 1;


    }
}
