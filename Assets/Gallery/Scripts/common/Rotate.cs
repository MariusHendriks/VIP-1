using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public enum Orientation { x, y, z }
    public Orientation orientation;
    public bool negative = false;
    public float speed = 1f;
    private float x = 0;
    private float y = 0;
    private float z = 0;

    // Update is called once per frame
    private void Start()
    {
        x = transform.eulerAngles.x;
        y = transform.eulerAngles.y;
        z = transform.eulerAngles.z;
        if (negative)
        {
            speed *= -1;
        }
    }
    void Update()
    {
        if (orientation == Orientation.x)
        {
            x += speed * Time.deltaTime * 10;
        }
        else if (orientation == Orientation.y)
        {
            y += speed * Time.deltaTime * 10;
        }
        else if (orientation == Orientation.z)
        {
            z += speed * Time.deltaTime * 10;
        }
        transform.eulerAngles = new Vector3(x, y, z);
    }
}
