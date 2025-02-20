using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    private Rigidbody2D rb;
    private Vector2 movement;
    public Animator playerAnim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input for movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Calling all the funtions Here
        animationPayer(); //--> This function use to play the player animations(ldle, walk)
        flipPlayerOnX(); //--> This function is use to flip the player on a x axis
    }

    void FixedUpdate()
    {
        // Move the player
        rb.velocity = movement.normalized * moveSpeed;
    }

    void animationPayer()
    {
        if (movement.x == 1 || movement.y == 1 || movement.x == -1 || movement.y == -1)
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
        if (movement.x <= -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x >= 1)
        {
            transform.localScale = Vector3.one;
        }
    }
}
