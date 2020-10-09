using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardIK : MonoBehaviour
{
    public GameObject targetObject;
    [Range(0.0f, 1.0f)]
    public float IKWeight = 0.8f;
    public bool rightHand = true;
    public bool leftHand = false;

    private Animator animator;
    private Transform rightTargetObject;
    private Transform leftTargetObject;
    private Vector3 rightTargetPos;
    private Vector3 leftTargetPos;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rightTargetObject = targetObject.transform.Find("RightHandTarget");
        leftTargetObject = targetObject.transform.Find("LeftHandTarget");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnAnimatorIK(int layerIndex)
    {

        if (rightHand && rightTargetObject != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, IKWeight);
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightTargetObject.position);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, IKWeight);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightTargetObject.rotation);
        }

        if (leftHand && leftTargetObject != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, IKWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftTargetObject.position);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, IKWeight);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftTargetObject.rotation);
        }
    }
}
