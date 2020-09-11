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
    private bool OtherCameraActive = false;
    private Transform selectedObject;
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
                selectedObject = _selection;
                selectedObject.GetComponent<RobotController>().enabled = true;
                selectedObject.GetComponent<RobotController>().robot.GetComponent<RobotMovement>().enabled = true;
                GameObject.Find("Character").GetComponent<FirstPersonController>().enabled = false;
                OtherCameraActive = true;
            }
            if (_selection != null && _selection.GetComponent<DodgeGameController>() != null)
            {
                selectedObject = _selection;
                selectedObject.GetComponent<DodgeGameController>().enabled = true;
                GameObject.Find("Character").GetComponent<FirstPersonController>().enabled = false;
                OtherCameraActive = true;
            }

        }

        if (Input.GetKey(KeyCode.Escape))
        {
            if (OtherCameraActive)
            {
                if (selectedObject.GetComponent<RobotController>() != null)
                {
                    selectedObject.GetComponent<RobotController>().enabled = false;
                    selectedObject.GetComponent<RobotController>().robot.GetComponent<RobotMovement>().enabled = false;
                }
                if (selectedObject.GetComponent<DodgeGameController>() != null)
                {
                    selectedObject.GetComponent<DodgeGameController>().enabled = false;
                }

                GameObject.Find("Character").GetComponent<FirstPersonController>().enabled = true;
                OtherCameraActive = false;
            }
        }

    }
}



