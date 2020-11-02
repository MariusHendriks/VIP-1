using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController2 : MonoBehaviour
{
    public Transform topOfJoystick;
    private float forwardBackwardTilt = 0;
    private float sideToSideTilt = 0;

    // Update is called once per frame
    void Update()
    {
        forwardBackwardTilt = topOfJoystick.rotation.eulerAngles.x;
        if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
        {
            forwardBackwardTilt = Mathf.Abs(forwardBackwardTilt - 360);
            //backward
        }
        else if (forwardBackwardTilt > 5 && forwardBackwardTilt < 74)
        {
            //forward
        }

        sideToSideTilt = topOfJoystick.rotation.eulerAngles.z;
        if (sideToSideTilt < 355 && sideToSideTilt > 290)
        {
            sideToSideTilt = Mathf.Abs(sideToSideTilt - 360);
            //left
        }
        else if (sideToSideTilt > 5 && sideToSideTilt < 74)
        {
            //right
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            transform.LookAt(other.transform.position, transform.up);
        }
    }
}
