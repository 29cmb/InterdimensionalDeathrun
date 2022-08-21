using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
    // Move player in 2D space
    private int maxSpeed = 10;
    private int jumpHeight = 10;
    private int gravityScale = 1;
    public Camera mainCamera;

    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    BoxCollider2D mainCollider;
    Transform t;
    public DynamicJoystick joystick;
    public Button JumpBtn;

    bool useJoystick = false;

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<BoxCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SystemInfo.deviceType != DeviceType.Desktop)
        {
            useJoystick = true;
            joystick.enabled = true;
            JumpBtn.enabled = true;
        }
        else
        {
            useJoystick = false;
            Destroy(joystick);
            Destroy(JumpBtn);
        }
        // Movement controls
        if (useJoystick == false && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            moveDirection = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) ? -1 : 1;
        }
        else
        {
            moveDirection = 0;
        }
            if (useJoystick == true)
            {
                if (joystick.Horizontal != 0 && joystick.Vertical != 0)
                {
                    moveDirection = (joystick.Horizontal > 0) ? 1 : -1;
                } else
                {
                    moveDirection = 0;
                }
            }
  

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        if (useJoystick == false && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }

        // Camera follow
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, t.position.y + 2, cameraPos.z);
        }
    }

    public void MobileJump()
    {
        if (isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }
    }

    async void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
        var currentScene = SceneManager.GetActiveScene().name;
        if (currentScene.Contains("Flipped"))
        {
            if (this.transform.position.y < -40)
            {
                await Task.Delay(100);
                Debug.Log("Respawning Player.");
                this.transform.position = GameObject.Find("RespawnPoint").transform.position;
            }
        } else
        {
            if (this.transform.position.y < -25)
            {
                await Task.Delay(100);
                this.transform.position = GameObject.Find("RespawnPoint").transform.position;
            }
        }
    }
}
