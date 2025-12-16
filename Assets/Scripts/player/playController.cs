using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

//This will be attached to the player gameobject to control its movement
public class PlayerController : MonoBehaviour
{
    //control variables
    //a speed value that will control how fast the player moves horizontally
    public float speed = 10f;
    public float groundCheckRadius = 0.02f;
    private bool isGrounded = false;


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

        float hValue = Input.GetAxis("Horizontal");
        float vValue = Input.GetAxisRaw("Vertical");

        SpriteFlip(hValue);


        rb.linearVelocityX = hValue * speed;
        Debug.Log("Velocity X: " + rb.linearVelocity.x);



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //apply an upward force to the rigidbody when the jump button is pressed
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }



        //update animator parameters
        anim.SetFloat ("hValue", Mathf.Abs(hValue));
        anim.SetBool  ("isGrounded", isGrounded);

    }

    private void OnValidate() => groundCheck?.UpdateGroundCheckRadius(groundCheckRadius);
        
    private void SpriteFlip(float hValue)
    {
        //flip the sprite based on movement direction
        if (hValue != 0)
            sr.flipX = (hValue < 0);
    }

}
