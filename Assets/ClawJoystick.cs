using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class ClawJoystick : MonoBehaviour
{
    public Transform bottom;
    public Transform handAttachment;
    public float maxOffset = 0.15f;

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
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

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
            Vector3 handPosNormalized = hand.transform.position;
            handPosNormalized = transform.InverseTransformPoint(handPosNormalized);
            handPosNormalized = Vector3.ClampMagnitude(handPosNormalized, maxOffset);
            handPosNormalized = transform.TransformPoint(handPosNormalized);
            handPosNormalized.y = startPos.y;
            bottom.rotation = Quaternion.LookRotation(handPosNormalized - bottom.position);
            bottom.rotation *= Quaternion.Euler(90, 0, 0);
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

    public Vector2 GetJoystickPosition()
    {
        return new Vector2(transform.localPosition.x, transform.localPosition.z) * 4;
    }
}
