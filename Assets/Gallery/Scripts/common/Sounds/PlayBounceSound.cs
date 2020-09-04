using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBounceSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        var audio = GetComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("Sounds/boing" + Random.Range(1, 4));
        audio.pitch = Random.Range(0.70f, 1.30f);
        audio.Play();
    }
}
