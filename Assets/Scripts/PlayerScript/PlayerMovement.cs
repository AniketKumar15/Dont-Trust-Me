using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    private Rigidbody2D rb;
    private Vector2 movement;
    public Animator playerAnim;

    [Header("Mobile Joystick Support")]
    public Joystick joystick; // Reference to UI Joystick (Attach in Inspector)
    public bool useJoystick = false; // Toggle joystick support

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if using joystick
        if (useJoystick && joystick != null)
        {
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;
        }
        else
        {
            // Get input for movement (PC)
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        // Calling functions
        animationPlayer();
        flipPlayerOnX();
    }

    void FixedUpdate()
    {
        // Move the player
        rb.velocity = movement.normalized * moveSpeed;
    }

    void animationPlayer()
    {
        if (movement.magnitude > 0) // Player is moving
        {
            playerAnim.SetBool("IsRunning", true);
        }
        else
        {
            playerAnim.SetBool("IsRunning", false);
        }
    }

    void flipPlayerOnX()
    {
        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x > 0)
        {
            transform.localScale = Vector3.one;
        }
    }
}
