using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRad : MonoBehaviour
{

    public float amount = 10f;

    void FixedUpdate()
    {


        if (Input.GetKey(KeyCode.E))
        {
            GetComponent<Rigidbody>().AddTorque(transform.right * amount);
        }

    }
}
