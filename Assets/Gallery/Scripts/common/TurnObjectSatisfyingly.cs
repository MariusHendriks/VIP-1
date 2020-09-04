using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnObjectSatisfyingly : MonoBehaviour
{
    public float xRotation = 8f;
    public float yRotation = 8f;
    private float x;
    private float y;
    private bool collided = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!collided)
        {
            x += xRotation * Time.deltaTime; // Needed to be done or else eulerangel would stop at 90 degrees
            y += yRotation * Time.deltaTime;
            transform.eulerAngles = new Vector3(x, y, 0);
        }


    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        collided = true;
    }
}
