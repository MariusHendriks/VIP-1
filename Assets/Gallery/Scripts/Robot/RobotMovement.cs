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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * movementSpeed / 50 * Input.GetAxis("Vertical"); //unity->inputmanager. Makes this work for controllers and keyboard.
        transform.position += transform.right * Time.deltaTime * movementSpeed / 50 * Input.GetAxis("Horizontal");


        float Swing = Mathf.PingPong(Time.time * movementSpeed, 160f) - 80f;
        float SwingBody = Mathf.PingPong(Time.time * (movementSpeed / 16), 10f) - 5f;

        LeftLeg.eulerAngles = new Vector3(Swing, LeftLeg.eulerAngles.y, LeftLeg.eulerAngles.z);
        RightLeg.eulerAngles = new Vector3(-Swing, RightLeg.eulerAngles.y, RightLeg.eulerAngles.z);
        LeftShoulder.eulerAngles = new Vector3(-Swing, LeftShoulder.eulerAngles.y, LeftShoulder.eulerAngles.z);
        RightShoulder.eulerAngles = new Vector3(Swing, RightShoulder.eulerAngles.y, RightShoulder.eulerAngles.z);
        Body.localEulerAngles = new Vector3(Body.localEulerAngles.x, SwingBody, Body.localEulerAngles.z);
    }
}
