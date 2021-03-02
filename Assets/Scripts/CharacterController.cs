using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;
    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadious;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;


    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpForce;
    public float wallJumpTime;
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();

    }
   void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadious, whatIsGround);
        moveInput = Input.GetAxisRaw("Horizontal");

        if(!wallSliding)       
          rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        
        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

       

    }

   void Update()
    {
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadious, whatIsGround);
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;

            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump")){
            isJumping = false;
        }

        if (isTouchingFront == true && isGrounded == false && moveInput != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }
        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (Input.GetButtonDown("Jump") && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }
        if (wallJumping)
        {
            rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        }

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void SetWallJumpingToFalse()
    {
        wallJumping = false;

    }
}
   