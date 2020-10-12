using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorHoldIK : MonoBehaviour
{
    public GameObject targetObject;
    [Range(0.0f, 1.0f)]
    public float IKWeight = 0.8f;
    public bool rightHand = true;
    public bool leftHand = false;

    private Animator animator;
    private Transform rightTargetObject;
    private Transform leftTargetObject;
    private Transform rightShoulderTargetObject;
    private Transform leftShoulderTargetObject;

    void Start()
    {
        animator = GetComponent<Animator>();
        rightShoulderTargetObject = targetObject.transform.Find("RightShoulderTarget");
        leftShoulderTargetObject = targetObject.transform.Find("LeftShoulderTarget");
        rightTargetObject = targetObject.transform.Find("RightHandTarget");
        leftTargetObject = targetObject.transform.Find("LeftHandTarget");
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (rightHand && rightTargetObject != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, IKWeight);
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightTargetObject.position);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, IKWeight);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightTargetObject.rotation);
            animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, IKWeight);
            animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightShoulderTargetObject.position);
        }

        if (leftHand && leftTargetObject != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, IKWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftTargetObject.position);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, IKWeight);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftTargetObject.rotation);
            animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, IKWeight);
            animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftShoulderTargetObject.position);
        }
    }
}
