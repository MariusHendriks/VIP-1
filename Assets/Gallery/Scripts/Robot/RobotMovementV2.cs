using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class RobotMovementV2 : MonoBehaviour
{

    public float movementSpeed = 2f;
    public float afkTimer = 10.0f;
    public float lowJumpMultiplier = 1.5f;
    public float fallMultiplier = 2f;

    public ParticleSystem JetpackSmokeLeft;
    public ParticleSystem JetpackSmokeRight;
    public ParticleSystem JetpackFireLeft;
    public ParticleSystem JetpackFireRight;
    public GameObject JetpackLightLeft;
    public GameObject JetpackLightRight;

    private float jetpackForce = 0.5f;
    private float startTime;
    private float InitialAfkTimer;
    private bool moved = false;
    private bool isDancing = false;

    private Animator animator;

    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 20f;
    private bool jump = false;
    private float vSpeed = 0;

    CharacterController cc;

    private void Start()
    {

        InitialAfkTimer = afkTimer;
        animator = GetComponent<Animator>();
        animator.enabled = true;

        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.forward * Time.deltaTime * movementSpeed / 50 * Input.GetAxis("Vertical");
        //transform.position += transform.right * Time.deltaTime * movementSpeed / 50 * Input.GetAxis("Horizontal");

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= movementSpeed;

        if (cc.isGrounded)
        {
            vSpeed = 0;
        }


        if (Input.GetButton("Jump"))
        {

            if (vSpeed < 6f)
            {
                vSpeed += jetpackForce * Time.deltaTime * 100f;
                if (jetpackForce < 6f)
                {
                    jetpackForce *= 1.01f;
                }
            }
        }
        else
        {
            jetpackForce = 0.1f;
            vSpeed -= gravity * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            StartParticles();
        }

        if (Input.GetButtonUp("Jump"))
        {
            StopParticles();
            jump = false;
        }

        moveDirection.y = vSpeed;
        cc.Move(moveDirection * Time.deltaTime);


        if (!cc.isGrounded)
        {

            animator.SetBool("isFlying", true);
        }
        else
        {
            animator.SetBool("isFlying", false);
        }

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            moved = false; //No movement

            animator.SetBool("isWalking", false);
        }
        else
        {
            ResetValues();
        }

        afkTimer -= Time.deltaTime;
        if (afkTimer < 0 && !isDancing)
        {
            isDancing = true;
            animator.SetBool("isDancing", true);
        }

    }

    void ResetValues()
    {
        startTime = Time.time;
        animator.SetBool("isDancing", false);
        animator.SetBool("isWalking", true);
        moved = true;
        afkTimer = InitialAfkTimer;
        isDancing = false;
    }

    void FixedUpdate()
    {
        var nVelocity = transform.InverseTransformDirection(cc.velocity).normalized;

        animator.SetFloat("xMotion", nVelocity.z);
        animator.SetFloat("yMotion", nVelocity.y);
    }
    private void OnDisable()
    {
        animator.SetBool("isWalking", false);
        ResetValues();
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
