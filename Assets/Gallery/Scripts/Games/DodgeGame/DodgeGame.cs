using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGame : MonoBehaviour
{
    private float scrollSpeed = 0.05f;
    private Material material;
    public float acceleration = 1.0f;
    private bool dead = false;
    // Start is called before the first frame update
    void Start()
    {

        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

        material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.DownArrow))
        {
            material.mainTextureOffset += new Vector2(0, -0.001f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            material.mainTextureOffset += new Vector2(0, 0.001f);
        }
        scrollSpeed += acceleration * Time.deltaTime * 0.01f;
        material.mainTextureScale += new Vector2(-(acceleration * Time.deltaTime * 0.002f), 0);
        dead = isDead(material.mainTextureOffset.x, material.mainTextureOffset.y);

        if (dead)
        {

        }


    }


    bool isDead(float x, float y)
    {

        x %= 1;




        if (x > 0.07f && x < 0.09f && (y < 0.17f || y > 0.35f))
        {
            return true;

        }
        else if (x > 0.32f && x < 0.34f && (y < 0.18f || y > 0.36f))
        {
            return true;
        }
        else if (x > 0.32f && x < 0.34f && (y < 0.18f || y > 0.36f))
        {
            return true;
        }
        else if (x > 0.445f && x < 0.465f && (y < 0.22f || y > 0.4f))
        {
            return true;
        }
        else if (x > 0.57f && x < 0.59f && (y < 0.03f || y > 0.21f))
        {
            return true;
        }
        else if (x > 0.695f && x < 0.715f && (y < 0.12f || y > 0.3f))
        {
            return true;
        }
        else if (x > 0.82f && x < 0.84f && (y < 0.17f || y > 0.35f))
        {
            return true;
        }
        else if (x > 0.945f && x < 0.965f && (y < 0.38f || y > 0.56f))
        {
            return true;
        }

        return false;
    }
}

