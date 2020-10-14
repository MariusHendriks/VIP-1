using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchAnimation : MonoBehaviour
{
    public bool animEnabled = false;
    public Transform watchDest = null;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void LateUpdate() {

        if(animEnabled && watchDest != null)
        {
            EyeLookAt(anim.GetBoneTransform(HumanBodyBones.LeftEye), watchDest);
            EyeLookAt(anim.GetBoneTransform(HumanBodyBones.RightEye), watchDest);
        }
    }

    void EyeLookAt(Transform eye, Transform dest)
    {
        float xRotation = eye.localEulerAngles.x;
        eye.LookAt(dest);
        eye.localRotation = Quaternion.Euler(xRotation, eye.localEulerAngles.y, eye.localEulerAngles.z);
    }

}
