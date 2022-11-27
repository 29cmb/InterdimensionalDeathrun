using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update

    public PolygonCollider2D SpikeHitbox;
    public BoxCollider2D PlayerHitbox;
    public ParticleSystem PlayerParticles;
    public AudioSource Audio;
    void Start()
    {//nothing to see here
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerHitbox.IsTouching(SpikeHitbox))
        {
            Audio.Play();
            PlayerParticles.Play();
            GameObject.Find("Player").transform.position = GameObject.Find("RespawnPoint").transform.position;
        }
    }
}
