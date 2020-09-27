using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGameController : Selectable
{
    public CameraManager cameraManager;
    public Camera gameCamera;
    public Camera playerCamera;
    // Start is called before the first frame update
    void OnEnable()
    {
        cameraManager.SwitchCamera(playerCamera, gameCamera);
        playerCamera.enabled = false;
    }

    void OnDisable()
    {
        cameraManager.SwitchCamera(gameCamera, playerCamera);
        playerCamera.enabled = true;
    }
}
