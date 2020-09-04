using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    public float movementSpeed = 200f;
    public Transform LeftLeg;
    public Transform RightLeg;
    public Transform LeftShoulder;
    public Transform RightShoulder;
    public Transform Body;
    private float startTime;
    private bool moved = false;


    public float jumpForce = 8f;

    private bool canJump = false;
    private bool jump = false;
    // Update is called once per frame
    void Update()
    {

        transform.position += transform.forward * Time.deltaTime * movementSpeed / 50 * Input.GetAxis("Vertical"); //unity->inputmanager. Makes this work for controllers and keyboard.
        transform.position += transform.right * Time.deltaTime * movementSpeed / 50 * Input.GetAxis("Horizontal");

        //starttime-time.time+0.4f is because this way it starts halfway through the 'animation'
        float Swing = Mathf.PingPong((startTime - Time.time + 0.4f) * movementSpeed, 160f) - 80f;
        float SwingBody = Mathf.PingPong((startTime - Time.time + 0.25f) * (movementSpeed / 16), 10f) - 5f;
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            moved = false;
            Swing = 0;
            SwingBody = 0;
        }
        else
        {
            if (!moved)
            {
                startTime = Time.time;
                moved = true;
            }

        }
        LeftLeg.eulerAngles = new Vector3(Swing, LeftLeg.eulerAngles.y, LeftLeg.eulerAngles.z);
        RightLeg.eulerAngles = new Vector3(-Swing, RightLeg.eulerAngles.y, RightLeg.eulerAngles.z);
        LeftShoulder.eulerAngles = new Vector3(-Swing, LeftShoulder.eulerAngles.y, LeftShoulder.eulerAngles.z);
        RightShoulder.eulerAngles = new Vector3(Swing, RightShoulder.eulerAngles.y, RightShoulder.eulerAngles.z);
        Body.localEulerAngles = new Vector3(Body.localEulerAngles.x, SwingBody, Body.localEulerAngles.z);

        if (Input.GetButtonDown("Jump") && canJump && !jump)
        {
            jump = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jump = false;
        }
    }

    void FixedUpdate()
    {
        if (jump && canJump)
        {
            canJump = false;
            GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }
}
