using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastControl : MonoBehaviour
{
    public float maxDistance = 100f;
    private bool interact = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interact = true;
        }
        else
        {
            interact = false;
        }
    }
    void FixedUpdate()
    {


        // Reycasting is pointing forward like if you would shoot something.
        // Will contain the information of which object the raycast hit
        RaycastHit hit;



        // if raycast hits, it checks if it hit an object with the tag Player
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            var tag = hit.transform.gameObject.tag;
            if (tag == "Button" && interact)
            {
                Debug.Log("Geniaal.");
            }

        }

    }
}
