using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Diagnostics;
using DG.Tweening;

public class ObjectManagerBlock : MonoBehaviour
{
    public bool Toggle_Enable;
    public GameObject InteractionItem;
    private BoxCollider2D PlayerCollision;
    private BoxCollider2D BoxCollision;
    private GameObject player;
    public Camera mainCamera;

    public Configuration config;

    private PlayerController controller;

    public bool isActive = false;

    public bool cameraSizeTween = false;
    private Rigidbody2D playerRigidbody;
    bool Debounce = false;
    void Start()
    {
        player = GameObject.Find("Player");
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        controller = player.GetComponent<PlayerController>();
        BoxCollision = GetComponent<BoxCollider2D>();
        PlayerCollision = player.GetComponent<BoxCollider2D>();
        if (Toggle_Enable == true)
        {
            InteractionItem.SetActive(false);
        }
    }

    // Update is called once per frame
    async void FixedUpdate()
    {
        player = config.player;
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        controller = player.GetComponent<PlayerController>();
        if (PlayerCollision.IsTouching(BoxCollision) && Debounce == false)
        {
            Debounce = true;
            if (cameraSizeTween == true) {
                if (isActive == false) {
                    isActive = true;
                    playerRigidbody.gameObject.SetActive(false);
                    mainCamera.DOOrthoSize(85f, 1.25f).OnComplete(async () => {
                        await Task.Delay(800);
                        InteractionItem.GetComponent<SpriteRenderer>().DOFade(0, .9f).SetEase(Ease.InOutSine);
                        InteractionItem.GetComponent<BoxCollider2D>().enabled = false;
                        await Task.Delay(2500);
                        mainCamera.DOOrthoSize(8.01823f, 1.25f);
                        playerRigidbody.gameObject.SetActive(true);
                    });
                    await Task.Delay(7000);
                    Debounce = false;
                }
                else
                {
                    isActive = false;
                    playerRigidbody.gameObject.SetActive(false);
                    mainCamera.DOOrthoSize(85f, 1.25f).OnComplete(async () => {
                        await Task.Delay(800);
                        InteractionItem.GetComponent<SpriteRenderer>().DOFade(1, .9f).SetEase(Ease.InOutSine);
                        InteractionItem.GetComponent<BoxCollider2D>().enabled = true;
                        await Task.Delay(2500);
                        mainCamera.DOOrthoSize(8.01823f, 1.25f);
                        playerRigidbody.gameObject.SetActive(true);
                    });
                    await Task.Delay(7000);
                    Debounce = false;
                }
               
                
            } else
            {
                if(isActive == false)
                {
                    isActive = true;
                    InteractionItem.GetComponent<SpriteRenderer>().DOFade(0, .9f).SetEase(Ease.InOutSine);
                    InteractionItem.GetComponent<BoxCollider2D>().enabled = false;
                    await Task.Delay(5000);
                    Debounce = false;
                }
                else
                {
                    isActive = false;
                    InteractionItem.GetComponent<SpriteRenderer>().DOFade(1, .9f).SetEase(Ease.InOutSine);
                    InteractionItem.GetComponent<BoxCollider2D>().enabled = true;
                    await Task.Delay(5000);
                    Debounce = false;
                }
                
            }
            
            
            
        }
    }
}
