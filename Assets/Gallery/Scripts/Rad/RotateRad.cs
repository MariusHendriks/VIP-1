using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTorque : MonoBehaviour
{

    public float pushSpeed = 1000f;

    void FixedUpdate()
    {


        if (Input.GetKey(KeyCode.E))
        {
            GetComponent<Rigidbody>().AddTorque(transform.right * pushSpeed);
        }

    }
}
