using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public variables
    private float jumpHeight = 4.0f;                // Height reached during a jump
    private float jumpDuration = 0.15f;              // Duration of a complete jump cycle
    private float acceleration = 3.5f;             // Acceleration while moving
    private float deceleration = 2.0f;             // Deceleration when no input is given
    private float cameraFollowSpeed = 5.0f;         // Speed at which the camera follows the player
    private float maxVerticalOffset = 2.0f;         // Maximum vertical offset of the camera from the player
    private float respawnHeight = -25.0f;           // Height at which the player will respawn
    public Transform respawnPoint;                 // Respawn point for the player
    private float maxSpeed = 12.0f; // Maximum speed of the player

    // Allow other scripts to manage the camera
    public bool overrideCamera = false;
    // Private variables
    public Rigidbody2D rb;
    private bool isJumping = false;
    private float jumpStartTime = 0.0f;
    private Camera mainCamera;
    private Vector3 initialScale;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the player is on the ground
        bool isGrounded = IsGrounded();

        // Update input and get movement direction
        float inputDirection = Input.GetAxis("Horizontal");

        // Apply acceleration or deceleration
        if (inputDirection != 0)
        {
            rb.AddForce(new Vector2(inputDirection * acceleration, 0));
        }
        else
        {
            rb.AddForce(new Vector2(-rb.velocity.x * deceleration, 0));
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        // Jumping logic
        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y)));
            isJumping = true;
            jumpStartTime = Time.time;
        }

        // Smooth jump animation
        if (isJumping)
        {
            float jumpTime = Time.time - jumpStartTime;
            if (jumpTime >= jumpDuration)
            {
                isJumping = false;
                transform.localScale = initialScale;
            }
            else
            {
                float scale = Mathf.Abs(jumpTime / jumpDuration);
                float scaleX = Mathf.Lerp(initialScale.x, initialScale.x * 1.25f, scale);
                float scaleY = Mathf.Lerp(initialScale.y, initialScale.y / 1.25f, scale);
                transform.localScale = new Vector3(scaleX, scaleY, initialScale.z);
            }
        }

        // Camera following logic
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
        float verticalOffset = Mathf.Clamp(transform.position.y - mainCamera.transform.position.y, -maxVerticalOffset, maxVerticalOffset);
        targetPosition += new Vector3(0f, verticalOffset, 0f);
        if(overrideCamera == false)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, cameraFollowSpeed * Time.deltaTime);
        }
        

        // Check if the player has reached the respawn height
        if (transform.position.y < respawnHeight)
        {
            Respawn();
        }
    }

    // Check if the player is on the ground
    private bool IsGrounded()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        Debug.Log("Number of platforms: " + platforms.Length);

        // Iterate over the platforms using a foreach loop
        foreach (GameObject platform in platforms)
        {
            if (rb.gameObject.GetComponent<BoxCollider2D>().IsTouching(platform.GetComponent<BoxCollider2D>())) {
                return true;
            }
        }
        return false;
    }

    // Respawn the player at the respawn point
    private void Respawn()
    {
        transform.position = respawnPoint.position;
        rb.velocity = Vector2.zero;
    }
}
