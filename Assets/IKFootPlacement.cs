using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootPlacement : MonoBehaviour
{
    [Range(0, 1f)]
    public float distanceBetweenToesAndAnkle;
    public LayerMask layerMask;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (anim)
        {
            SetFootPosition(AvatarIKGoal.LeftFoot);
            SetFootPosition(AvatarIKGoal.RightFoot);
        }
    }

    private void SetFootPosition(AvatarIKGoal foot)
    {
        anim.SetIKPositionWeight(foot, 1f);
        anim.SetIKRotationWeight(foot, 1f);

        RaycastHit hit;
        Ray ray = new Ray(anim.GetIKPosition(foot) + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out hit, distanceBetweenToesAndAnkle + 1f, layerMask) && hit.transform.tag == "Walkable")
        {
            Vector3 footPosition = hit.point;
            footPosition.y += distanceBetweenToesAndAnkle;
            anim.SetIKPosition(foot, footPosition);
            anim.SetIKRotation(foot, Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation);
        }
    }
}
