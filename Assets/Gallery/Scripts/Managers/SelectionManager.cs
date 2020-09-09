using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SelectionManager : MonoBehaviour
{

    public Material highlightMaterial;
    private string selectableTag = "Selectable";
    private Material previousMaterial;
    private Transform _selection;
    private bool inRobot = false;
    private Transform selectedRobot;
    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null)
        {
            return;
        }
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = previousMaterial;
            _selection = null;
        }

        var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //get the blue axis (z)
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform; //selection is now the transform of whatever object you are looking at. (raycast hit)
            if (selection.CompareTag(selectableTag) && _selection != selection)
            {
                var selectionsRenderer = selection.GetComponent<Renderer>();
                if (selectionsRenderer != null)
                {
                    previousMaterial = selectionsRenderer.material;
                    selectionsRenderer.material = highlightMaterial;
                }

                _selection = selection;
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_selection != null && _selection.GetComponent<RobotController>() != null)
            {
                selectedRobot = _selection;
                selectedRobot.GetComponent<RobotController>().enabled = true;
                selectedRobot.GetComponent<RobotController>().robot.GetComponent<RobotMovement>().enabled = true;
                GameObject.Find("Character").GetComponent<FirstPersonController>().enabled = false;
                inRobot = true;
            }

        }

        if (Input.GetKey(KeyCode.Escape))
        {
            if (inRobot)
            {
                selectedRobot.GetComponent<RobotController>().enabled = false;
                selectedRobot.GetComponent<RobotController>().robot.GetComponent<RobotMovement>().enabled = false;
                GameObject.Find("Character").GetComponent<FirstPersonController>().enabled = true;
                inRobot = false;
            }
        }

    }
}



