using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClawMachineController : Selectable
{
    public CameraManager cameraManager;
    public Camera playerCamera;
    public Camera gameCamera;
    public ClawMachineMovement clawMachineMovement;
    // Start is called before the first frame update
    void OnEnable()
    {
        cameraManager.SwitchCamera(playerCamera, gameCamera);
        clawMachineMovement.enabled = true;
    }
    void OnDisable()
    {
        cameraManager.SwitchCamera(gameCamera, playerCamera);
        clawMachineMovement.enabled = false;
    }

}
