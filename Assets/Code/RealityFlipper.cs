using UnityEngine;
using DG.Tweening;

public class RealityFlipper : MonoBehaviour
{
    public float flipDuration = 2.0f; // Duration of the flip animation in seconds
    public float flipDelay = 10.0f; // Delay after the flip before the RealityFlipper can be used again
    
    private Transform flipperPosition;
    private bool canFlip = true; // Flag indicating whether the RealityFlipper can be used
    private Renderer renderer; // Renderer component to change the mesh color
    private Transform parentTransform; // Parent transform to flip the entire workspace
    private Rigidbody2D playerRigidbody;
    public PlayerController playerControl;
    public Camera mainCamera;
    public float zoomDistance = 60f;

    public Configuration config;
    private GameObject player; // Reference to the player object
    private void Start()
    {
        
        renderer = GetComponent<Renderer>();
        parentTransform = transform.parent;
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerController>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        flipperPosition = this.transform;
    }

    private void Update()
    {
        player = config.player;
        playerControl = player.GetComponent<PlayerController>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (canFlip && IsPlayerTouching())
        {
            canFlip = false;
            playerControl.overrideCamera = true; // Set overrideCamera to true
            playerRigidbody.gameObject.SetActive(false);
            // Zoom out the camera to show the whole map
            mainCamera.DOOrthoSize(zoomDistance, flipDuration).OnComplete(() => {
                Debug.Log(config.isFlipped);
                FlipMap(); // Flip the map after the delay
            });
        }
    }

    private void FlipMap()
    {
        if (config.isFlipped == false)
        {
            Debug.Log("IsFlipped is called as false");
            parentTransform.DORotate(new Vector3(0, 0, 180), flipDuration).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                Debug.Log("DORotate called");
                config.isFlipped = true;
                player.transform.position = flipperPosition.position - new Vector3(0f, 2f, 0f); // Teleport the player
                playerRigidbody.gameObject.SetActive(true); // Re-enable the player sprite
                playerControl.overrideCamera = false; // Set overrideCamera to false
                mainCamera.DOOrthoSize(8.01823f, flipDuration);
                renderer.material.color = Color.gray; // Set the mesh color to gray
                Invoke(nameof(EnableFlipper), flipDelay); // Enable the RealityFlipper after the delay
            });
        }
        else if (config.isFlipped == true)
        {
            Debug.Log("IsFlipped is called as true");
            parentTransform.DORotate(new Vector3(0, 0, 0), flipDuration).SetEase(Ease.InOutSine).OnComplete(() => {
                config.isFlipped = false;

                player.transform.position = flipperPosition.position - new Vector3(0f, 2f, 0f); // Teleport the player
                playerRigidbody.gameObject.SetActive(true); // Re-enable the player sprite
                playerControl.overrideCamera = false; // Set overrideCamera to false
                mainCamera.DOOrthoSize(9f, flipDuration);
                renderer.material.color = Color.gray; // Set the mesh color to gray
                Invoke(nameof(EnableFlipper), flipDelay); // Enable the RealityFlipper after the delay
            });
        }

    }

    private bool IsPlayerTouching()
    {
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        BoxCollider2D flipperCollider = GetComponent<BoxCollider2D>();

        return playerCollider.IsTouching(flipperCollider);
    }

    private void EnableFlipper()
    {
        canFlip = true;
        renderer.material.color = Color.white; // Reset the mesh color
    }
}