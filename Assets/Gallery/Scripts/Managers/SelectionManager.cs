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

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
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
            _selection.GetComponent<RobotController>().enabled = true;
            _selection.GetComponent<RobotController>().robot.GetComponent<RobotMovement>().enabled = true;
            GameObject.Find("Character").GetComponent<FirstPersonController>().enabled = false;
        }

    }
}



