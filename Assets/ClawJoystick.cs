using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class ClawJoystick : MonoBehaviour
{
    public Transform bottom;
    public Transform handAttachment;

    private Vector3 oldPosition;
    private Quaternion oldRotation;
    protected Hand.AttachmentFlags attachmentFlags = (~Hand.AttachmentFlags.SnapOnAttach) & Hand.AttachmentFlags.TurnOnKinematic;
    private Interactable interactable;
    private bool maxRotationReached = false;
    void Awake()
    {
        interactable = this.GetComponent<Interactable>();
    }

    private bool isAttached;
    private Quaternion previousRotation;

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            // Save our position/rotation so that we can restore it when we detach
            oldPosition = transform.position;
            oldRotation = transform.rotation;

            // Call this to continue receiving HandHoverUpdate messages,
            // and prevent the hand from hovering over anything else
            hand.HoverLock(interactable);

            // Attach this object to the hand
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);

            isAttached = true;

        }
        else if (isGrabEnding)
        {
            // Detach this object from the hand
            hand.DetachObject(gameObject);

            // Call this to undo HoverLock
            hand.HoverUnlock(interactable);

            // Restore position/rotation
            transform.position = oldPosition;
            transform.rotation = oldRotation;

            isAttached = false;
        }

        if (isAttached)
        {
            Vector3 rotationDistance = new Vector3(0, 0.3f, 0) - transform.localPosition;
            if (rotationDistance.magnitude > 0.2)
            {
                Vector3 handPosNormalized = transform.InverseTransformPoint(hand.transform.position);
                handPosNormalized.y = 0.3f;
                Vector3 vectorToHand = new Vector3(0, 0.3f, 0) - handPosNormalized;
                vectorToHand = vectorToHand.normalized * 0.2f;
                bottom.rotation = Quaternion.LookRotation(transform.TransformPoint(vectorToHand) - bottom.position);
                bottom.rotation *= Quaternion.Euler(90, 0, 0);
            }
            else
            {
                previousRotation = bottom.rotation;
                bottom.rotation = Quaternion.LookRotation(hand.transform.position - bottom.position);
                bottom.rotation *= Quaternion.Euler(90, 0, 0);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = handAttachment.position;

        if (!isAttached)
        {
            bottom.rotation = Quaternion.Lerp(bottom.rotation, Quaternion.Euler(0, 0, 0), 2 * Time.deltaTime);
        }

    }
}
