using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public void SwitchCamera(Camera prevCam, Camera cameraToSwitchTo)
    {
        prevCam.enabled = false;
        cameraToSwitchTo.enabled = true;
    }
}
