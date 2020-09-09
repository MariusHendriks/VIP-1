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
    public Transform UpperBody;
    public Transform Body;
    private float startTime;
    public float afkTimer = 10.0f;
    private float InitialAfkTimer;
    private bool moved = false;
    private bool hasDanced = false;

    public float jumpForce = 8f;
    public GameObject RobotCameraHolder;
    private bool canJump = false;
    private bool jump = false;


    //Still ugly with lack of knowledge on how to do it better :D
    private Vector3 dancePosition = new Vector3(0, 0, 0);
    private Vector3 dancePositionUpperBody = new Vector3(0, 0, 0);


    private void Start()
    {
        InitialAfkTimer = afkTimer;
    }
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

                ResetPosition();
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


        afkTimer -= Time.deltaTime;
        if (afkTimer < 0 && !hasDanced)
        {
            Dance();
        }

    }
    void Dance()
    {
        //Ugly but works :)

        RobotCameraHolder.GetComponent<RobotCamera>().rotateCamera = true;
        if (dancePosition.x < 25)
        {
            dancePosition += new Vector3(20f * Time.deltaTime, 0, 0);
            Body.localEulerAngles = dancePosition;
        }
        else if (dancePositionUpperBody.y < 180)
        {
            dancePositionUpperBody += new Vector3(0, 60f * Time.deltaTime, 0);
            UpperBody.localEulerAngles = dancePositionUpperBody;
        }


        // 20f * Time.deltaTime
    }
    void ResetPosition()
    {
        startTime = Time.time;
        moved = true;

        RobotCameraHolder.GetComponent<RobotCamera>().rotateCamera = false;
        afkTimer = InitialAfkTimer;
        UpperBody.localEulerAngles = new Vector3(0, 0, 0);
        Body.localEulerAngles = new Vector3(0, 0, 0);
        LeftShoulder.localEulerAngles = new Vector3(0, 0, 0);
        RightShoulder.localEulerAngles = new Vector3(0, 0, 0);
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
