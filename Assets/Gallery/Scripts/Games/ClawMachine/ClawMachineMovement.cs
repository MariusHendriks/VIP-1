using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ClawMachineMovement : MonoBehaviour
{
    public float speedLeftAndRight = 0.2f;
    public float speedUpAndDown = 0.2f;
    public HingeJoint center;
    public HingeJoint left;
    public HingeJoint right;
    public GameObject zPlate;
    public GameObject xPlate;
    public GameObject hole;

    private bool reachedBottom = false;
    private bool grabSequenceActivated = false;
    private bool reachedTop = false;
    private bool hingeClosed = false;
    private bool hingeOpened = false;
    private float initialHeight;
    private bool objectReleased;

    void Start()
    {
        initialHeight = transform.localPosition.y;
    }

    void FixedUpdate()
    {
        zPlate.transform.localPosition = new Vector3(zPlate.transform.localPosition.x, zPlate.transform.localPosition.y, transform.localPosition.z);
        xPlate.transform.localPosition = new Vector3(transform.localPosition.x, xPlate.transform.localPosition.y, xPlate.transform.localPosition.z);
        hole.transform.localPosition = new Vector3(transform.localPosition.x, hole.transform.localPosition.y, transform.localPosition.z);

        if (!grabSequenceActivated)
        {
            transform.position += transform.right * Time.deltaTime * speedLeftAndRight * -Input.GetAxis("Vertical");
            transform.position += transform.forward * Time.deltaTime * speedLeftAndRight * Input.GetAxis("Horizontal");

            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -2.5f, 2.5f), transform.localPosition.y, Mathf.Clamp(transform.localPosition.z, -2.5f, 2.5f));
        }



        if (grabSequenceActivated)
        {
            GrabSequence();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            grabSequenceActivated = true;
        }
    }

    private void GrabSequence()
    {
        if (transform.localPosition.y > 1.4f && !reachedBottom)
        {
            transform.position += new Vector3(0, -speedUpAndDown * Time.deltaTime, 0);
            HandleHinges(true, -25f);
        }
        else
        {
            reachedBottom = true;
            if (!hingeClosed && !reachedTop)
            {
                HandleHinges(false, 10f);
            }
            else
            {
                if (transform.localPosition.y < initialHeight && reachedBottom && !reachedTop)
                {
                    transform.position += new Vector3(0, speedUpAndDown * Time.deltaTime, 0);
                }
                else
                {
                    reachedTop = true;
                    if (reachedTop)
                    {
                        if (transform.localPosition.x <= 2.5)
                        {
                            transform.position += new Vector3(speedUpAndDown * Time.deltaTime, 0, 0);
                        }
                        if (transform.localPosition.z <= 2.5)
                        {
                            transform.position += new Vector3(0, 0, speedUpAndDown * Time.deltaTime);
                        }
                        if (transform.localPosition.z > 2.5 && transform.localPosition.x > 2.5)
                        {
                            if (!hingeOpened && !objectReleased)
                            {
                                HandleHinges(true, -25f);
                            }
                            else
                            {
                                objectReleased = true;
                                if (!hingeClosed)
                                {
                                    HandleHinges(false, 10f);
                                }
                                else
                                {
                                    objectReleased = false;
                                    grabSequenceActivated = false;
                                    reachedTop = false;
                                    reachedBottom = false;
                                }

                            }

                        }
                    }
                }
            }
        }
    }

    private void HandleHinges(bool open, float maxToGoTo)
    {
        var jointLimits = center.limits;
        if (open)
        {
            hingeClosed = false;
            if (jointLimits.max > maxToGoTo)
            {
                jointLimits.max -= 30f * Time.deltaTime;
            }
            else
            {
                hingeOpened = true;
            }
        }
        else
        {
            hingeOpened = false;
            if (jointLimits.max < maxToGoTo)
            {
                jointLimits.max += 40f * Time.deltaTime;

            }
            else
            {
                hingeClosed = true;
            }
        }
        center.limits = jointLimits;
        left.limits = jointLimits;
        right.limits = jointLimits;
    }
}

