using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class DodgeGame : MonoBehaviour
{
    public TextMeshPro score;
    public TextMeshPro highScore;
    private float scrollSpeed = 0.05f;
    private Material material;
    public float acceleration = 1.0f;
    private bool dead = false;
    private bool gameStarted = false;
    private float initialAcceleration;
    private float initialScrollspeed;
    private Vector2 initialOffset;
    public LinearMapping linearMapping;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        initialOffset = material.mainTextureOffset;
        initialAcceleration = acceleration;
        initialScrollspeed = scrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {

            material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);
            score.text = Mathf.Round(material.mainTextureOffset.x * 100).ToString();
            scrollSpeed += acceleration * Time.deltaTime * 0.01f;
            dead = isDead(material.mainTextureOffset.x, material.mainTextureOffset.y);
        }

        if (dead)
        {
            ResetGame();
        }



        material.mainTextureOffset = new Vector2(material.mainTextureOffset.x, (linearMapping.value) / 2);
    }

    public void StartGame()
    {
        if (!gameStarted)
        {
            score.text = "0";
            gameStarted = true;
        }
    }
    private void ResetGame()
    {
        gameStarted = false;
        if (float.Parse(score.text) > float.Parse(highScore.text))
        {
            highScore.text = score.text;
        }
        acceleration = initialAcceleration;
        scrollSpeed = initialScrollspeed;
        material.mainTextureOffset = initialOffset;
    }

    bool isDead(float x, float y)
    {
        x %= 1;
        if (x > 0.07f && x < 0.09f && (y < 0.17f || y > 0.35f))
        {
            return true;

        }
        else if (x > 0.195f && x < 0.215f && (y < 0.08f || y > 0.28f))
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

