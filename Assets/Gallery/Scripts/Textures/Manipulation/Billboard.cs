using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform LookAt;
    public bool lockY = false;
    void Update()
    {
        if (lockY)
        {
            var lookPos = new Vector3(LookAt.position.x, transform.position.y, LookAt.position.z);
            transform.LookAt(lookPos);
        }
        else
        {
            transform.LookAt(LookAt);
        }
        transform.rotation *= Quaternion.Euler(0, 180, 0);
    }
}
