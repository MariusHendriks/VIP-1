using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRad : MonoBehaviour
{
    private float x;
    private GameObject Lever;
    private Rigidbody leverRigidBody;

    void Start()
    {
        Lever = GameObject.Find("Lever");
        leverRigidBody = Lever.GetComponent<Rigidbody>();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            leverRigidBody.AddTorque(Lever.transform.forward * 100f);
        }


    }
}
