using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

        float offset = Time.time * scrollSpeed;
        material.mainTextureOffset = new Vector2(offset, 0);
    }
}
