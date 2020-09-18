using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform LookAt;

    void Update()
    {

        transform.LookAt(LookAt);
        transform.rotation *= Quaternion.Euler(0, 180, 0);
    }
}
