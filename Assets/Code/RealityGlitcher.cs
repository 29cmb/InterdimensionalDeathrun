using UnityEngine;
using DG.Tweening;

public class RealityGlitcher : MonoBehaviour
{
    public float flipDuration = 2.0f;        // Duration of the flip animation in seconds
    public float flipDelay = 10.0f;
    private GameObject player; // Delay after the flip before the RealityFlipper can be used again
    public GameObject parent;                // Reference to the player object
    private bool canFlip = true;             // Flag indicating whether the RealityFlipper can be used
    private Renderer renderer;               // Renderer component to change the mesh color
    private Transform parentTransform;       // Parent transform to flip the entire workspace
    private Rigidbody2D playerRigidbody;
    private PlayerController playerControl;
    public Camera mainCamera;

    public float zoomDistance = 50f;



    public Configuration config;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        parentTransform = transform.parent;
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerController>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        player = config.player;
        playerControl = player.GetComponent<PlayerController>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (canFlip && IsPlayerTouching())
        {
            canFlip = false;
            playerControl.overrideCamera = true;    // Set overrideCamera to true
            playerRigidbody.gameObject.SetActive(false);
            // Zoom out the camera to show the whole map
            mainCamera.DOOrthoSize(zoomDistance, flipDuration).OnComplete(() =>
            {
                GlitchMap();                        // Flip the map after the delay
            });
        }
    }

    private void GlitchMap()
    {
        if (config.isGlitched == false)
        {
            config.isGlitched = true;
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                GameObject childObject = parent.transform.GetChild(i).gameObject;
                childObject.GetComponent<BoxCollider2D>().enabled = false;
                childObject.GetComponent<SpriteRenderer>().DOFade(0.3f, .9f).SetEase(Ease.InOutSine);
            }
            renderer.material.color = Color.gray;
            Invoke(nameof(ResetCam), 1.5f);
            Invoke(nameof(EnableFlipper), flipDelay + 1.5f);
        }
        else {
            config.isGlitched = false;
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                GameObject childObject = parent.transform.GetChild(i).gameObject;
                childObject.GetComponent<BoxCollider2D>().enabled = true;
                childObject.GetComponent<SpriteRenderer>().DOFade(1f, .9f).SetEase(Ease.InOutSine);
            }
            renderer.material.color = Color.gray;
            Invoke(nameof(ResetCam), 1.5f);
            Invoke(nameof(EnableFlipper), flipDelay + 1.5f);
        }
      
    }
        
 


    private bool IsPlayerTouching()
    {
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        BoxCollider2D flipperCollider = GetComponent<BoxCollider2D>();

        return playerCollider.IsTouching(flipperCollider);
    }

    private void ResetCam()
    {
        mainCamera.DOOrthoSize(8.01823f, flipDuration);
        playerControl.overrideCamera = false;
        playerRigidbody.gameObject.SetActive(true);
    }

    private void EnableFlipper()
    {
        canFlip = true;
        renderer.material.color = Color.white;  // Reset the mesh color
    }
}
