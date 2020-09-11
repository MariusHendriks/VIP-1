using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGameController : MonoBehaviour
{

    public Camera gameCamera;
    public Camera mainCamera;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject.Find("GameSystem").GetComponent<CameraManager>().SwitchCamera(mainCamera, gameCamera);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("GameSystem").GetComponent<CameraManager>().SwitchCamera(gameCamera, mainCamera);
        }
    }
}
