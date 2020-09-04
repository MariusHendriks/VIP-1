using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOnCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}
