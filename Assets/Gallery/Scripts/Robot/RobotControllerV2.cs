using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControllerV2 : Selectable
{
    public GameObject robot;
    public Camera robotCamera;
    public Camera mainCamera;
    public CameraManager cameraManager;

    // Start is called before the first frame update
    void OnEnable()
    {
        robot.transform.Find("Cameraholder").gameObject.SetActive(true);
        cameraManager.SwitchCamera(mainCamera, robotCamera);
        robot.GetComponent<RobotMovementV2>().enabled = true;
    }

    void OnDisable()
    {
        robot.transform.Find("Cameraholder").gameObject.SetActive(false);
        cameraManager.SwitchCamera(robotCamera, mainCamera);
        robot.GetComponent<RobotMovementV2>().enabled = false;
    }
}
