using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCamera : MonoBehaviour
{
    public bool rotateCamera = false;
    public float speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked; //locks mouse


        if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0))
        {
            float y = transform.rotation.eulerAngles.y;
            transform.parent.eulerAngles = new Vector3(0, y, 0);
        }
        if (Input.GetMouseButton(0))  //lmb
        {
            transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * speed, Input.GetAxis("Mouse X") * speed, 0)); //Turn around the axis of the robot (parent)

            float x = transform.rotation.eulerAngles.x;
            if (x > 50f && x < 180f) x = 50f; //blocks at certain angle
            if (x < 330f && x > 180f) x = 330f;

            // Fix camera z rotation
            float y = transform.rotation.eulerAngles.y;
            transform.eulerAngles = new Vector3(x, y, 0);
        }
        else if (Input.GetMouseButton(1)) //rmb
        {
            //Rotate Robot
            transform.parent.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * speed, 0)); //>

            // Rotate camera
            transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * 0, 0, 0));

            // Set camera min and max x rotation
            float x = transform.rotation.eulerAngles.x;
            if (x > 50f && x < 180f) x = 50f;
            if (x < 330f && x > 180f) x = 330f;


            // Fix camera z and y rotation
            transform.localEulerAngles = new Vector3(x, 0, 0);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (rotateCamera)
        {
            transform.eulerAngles += new Vector3(0, 20f * Time.deltaTime, 0); // x and z have to be 0 or else woosh
        }
    }
}
