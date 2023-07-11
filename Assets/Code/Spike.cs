using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update

    public PolygonCollider2D SpikeHitbox;
    private BoxCollider2D PlayerHitbox;
    public ParticleSystem PlayerParticles;
    public AudioSource Audio;

    public Configuration config;
    async void Start()
    {//nothing to see here
        await Task.Delay(1000);
        PlayerHitbox = config.player.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerHitbox = config.player.GetComponent<BoxCollider2D>();
        if (PlayerHitbox.IsTouching(SpikeHitbox))
        {
            Audio.Play();
            PlayerParticles.Play();
            GameObject.Find("Player").transform.position = GameObject.Find("RespawnPoint").transform.position;
        }
    }
}
