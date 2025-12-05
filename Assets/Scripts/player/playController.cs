using UnityEngine;

//This will be attached to the player gameobject to control its movement
public class PlayerController : MonoBehaviour
{
    //control variables
    //a speed value that will control how fast the player moves horizontally
    public float speed = 10f;
    public float groundCheckRadius = 0.02f;
    private bool isGrounded = false;
    private bool isFiring = false;
    private float hValue = 0f;
    private bool canMove = true;
    private bool victory = false;

    private Vector2 groundCheckPos => new Vector2(col.bounds.center.x, col.bounds.min.y);


    //Component references
    //public Transform groundCheck;
    Rigidbody2D rb;
    Collider2D col;
    SpriteRenderer sr;
    Animator anim;
    GroundCheck groundCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get the Rigidbody2D component attached to the same gameobject - we assume that it exists
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        groundCheck = new GroundCheck(col, LayerMask.GetMask("Ground"), groundCheckRadius); 


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
        isGrounded = groundCheck.CheckIsGrounded();

        if (canMove)
        {
            Moving();
        }
        else
        {
            rb.linearVelocityX = 0;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //apply an upward force to the rigidbody when the jump button is pressed
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }

        //hold press e and jump to emote
        if (Input.GetKeyDown(KeyCode.E) && Input.GetButton("Jump"))
        {
            victory = true;
        }

        else if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("Jump"))
        {
            victory = false;
        }

        if (Input.GetButtonDown("fire") && isGrounded)
        {
            isFiring = true;
            Debug.Log("Firing!");
            canMove = false;
            hValue = 0f;
        }


        if (Input.GetButtonUp("fire"))
        {
            isFiring = false;
            Debug.Log("Stopped Firing!");
            canMove = true;
        }

        //update animator parameters
        anim.SetBool  ("Attack", isFiring);
        anim.SetFloat ("hValue", Mathf.Abs(hValue));
        anim.SetBool  ("isGrounded", isGrounded);
        anim.SetBool  ("isMoving", canMove);
        anim.SetBool  ("Victory", victory);
    }

    private void OnValidate() => groundCheck?.UpdateGroundCheckRadius(groundCheckRadius);
        
    private void SpriteFlip(float hValue)
    {
        //flip the sprite based on movement direction
        if (hValue != 0)
            sr.flipX = (hValue < 0);
    }

    private void Moving()
    {
        //grab our horizontal input value - negative button is moving to the left (A/Left Arrow), positive button is moving to the right (D/Right Arrow) - cross platform compatible so it works with keyboard, joystick, etc. -1 to 1 range where zero means no input
        hValue = Input.GetAxis("Horizontal");

        //flip the sprite based on movement direction
        SpriteFlip(hValue);

        //set the rigidbody's horizontal velocity based on the input value multiplied by our speed - vertical velocity remains unchanged
        rb.linearVelocityX = hValue * speed;
        Debug.Log("Velocity X: " + rb.linearVelocity.x);
    }
}