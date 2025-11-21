using UnityEngine;

//This will be attached to the player gameobject to control its movement
public class PlayerController : MonoBehaviour
{
    //control variables
    //a speed value that will control how fast the player moves horizontally
    public float speed = 10f;
    public float groundCheckRadius = 0.02f;
    private bool isGrounded = false;
    private Vector2 groundCheckPos => new Vector2(col.bounds.center.x, col.bounds.min.y);

    //layer mask to identify what is ground
    private LayerMask groundLayer;

    //Component references
    //public Transform groundCheck;
    Rigidbody2D rb;
    Collider2D col;
    SpriteRenderer sr;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get the Rigidbody2D component attached to the same gameobject - we assume that it exists
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();


        groundLayer = LayerMask.GetMask("Ground");

        //Transform based ground check setup - using an empty gameobject as a child of the player to define the ground check position
        //initalize ground check poositon using separate gameobject as a child of the player
        //GameObject newObj = new GameObject("GroundCheck");
        //newObj.transform.SetParent(transform);
        //newObj.transform.localPosition = Vector3.zero;
        //groundCheck = newObj.transform;
        //this is basically the same as doing it in the editor, but we do it here to keep everything self-contained

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);

        //grab our horizontal input value - negative button is moving to the left (A/Left Arrow), positive button is moving to the right (D/Right Arrow) - cross platform compatible so it works with keyboard, joystick, etc. -1 to 1 range where zero means no input
        float hValue = Input.GetAxis("Horizontal");

        SpriteFlip(hValue);

        //set the rigidbody's horizontal velocity based on the input value multiplied by our speed - vertical velocity remains unchanged
        rb.linearVelocityX = hValue * speed;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //apply an upward force to the rigidbody when the jump button is pressed
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }
    }

    private void SpriteFlip(float hValue)
    {
        if (hValue != 0)
            sr.flipX = (hValue < 0);
    }
}