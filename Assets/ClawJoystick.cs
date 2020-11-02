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
            bottom.rotation = Quaternion.LookRotation(hand.transform.position - bottom.position);
            bottom.rotation *= Quaternion.Euler(90, 0, 0);

            Vector3 rotationDistance = new Vector3(0, 0.3f, 0) - transform.localPosition;
            if (rotationDistance.magnitude > 0.2)
            {
                Debug.Log(previousRotation);
                bottom.rotation = previousRotation;
            }
            else
            {
                previousRotation = bottom.rotation;
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
