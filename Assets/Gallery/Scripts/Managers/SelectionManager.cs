using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SelectionManager : MonoBehaviour
{
    public GameObject mainCharacter;
    public Material highlightMaterial;


    private string selectableTag = "Selectable";
    private Material previousMaterial;
    private Transform _selection;
    private Selectable currentSelection;

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
        {
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
        }


        if (Input.GetKeyDown(KeyCode.E) && _selection != null && _selection.GetComponent<Selectable>() != null)
        {

            mainCharacter.GetComponent<FirstPersonController>().enabled = false;
            currentSelection = _selection.GetComponent<Selectable>();
            currentSelection.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && currentSelection != null)
        {
            mainCharacter.GetComponent<FirstPersonController>().enabled = true;
            currentSelection.enabled = false;
        }
    }
}



