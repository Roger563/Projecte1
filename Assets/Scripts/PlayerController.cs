﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    private float moveInput;
    private Rigidbody2D rb;

    private bool facingRight = true;

    public float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;

    bool wallJumping;
    public float wallJumpTime;
    public Vector2 wJForce = new Vector2(); //new & unused

    bool wallSliding;
    public float wallSlidingSpeed;

    //Collision detection
    private bool grounded;
    private bool leftWalled;
    private bool rightWalled;

    public float distanceDetection;
    Vector2 size;
    public LayerMask maskGround;

    void Awake()
    {
        size = new Vector2(GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.y);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        Detection();
        Movement();
    }

    void Update()
    {
        Jump();
        WallSliding();
        WallJump();     //empty
    }

    void Detection()
    {
        Vector2 downRayL = (Vector2)transform.position + Vector2.down * size * 0.5f + Vector2.left * size * 0.5f;   //down
        Vector2 downRayR = (Vector2)transform.position + Vector2.down * size * 0.5f + Vector2.right * size * 0.5f;
        Vector2 leftRayU = (Vector2)transform.position + Vector2.left * size * 0.5f + Vector2.up * size * 0.5f;     //left
        Vector2 leftRayD = (Vector2)transform.position + Vector2.left * size * 0.5f + Vector2.down * size * 0.5f;
        Vector2 rightRayU = (Vector2)transform.position + Vector2.right * size * 0.5f + Vector2.up * size * 0.5f;   //right
        Vector2 rightRayD = (Vector2)transform.position + Vector2.right * size * 0.5f + Vector2.down * size * 0.5f;

        grounded = (Physics2D.Raycast(downRayL, Vector2.down, distanceDetection, maskGround) || Physics2D.Raycast(downRayR, Vector2.down, distanceDetection, maskGround));
        leftWalled = (Physics2D.Raycast(leftRayU, Vector2.left, distanceDetection, maskGround) || Physics2D.Raycast(leftRayD, Vector2.left, distanceDetection, maskGround));
        rightWalled = (Physics2D.Raycast(rightRayU, Vector2.right, distanceDetection, maskGround) || Physics2D.Raycast(rightRayD, Vector2.right, distanceDetection, maskGround));
    }
    void Movement()
    {
        if (!wallJumping)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
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
            } else {
                isJumping = false;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }
    void WallSliding()
    {
        if ((leftWalled || rightWalled) && grounded == false && moveInput != 0)
        {
            wallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            wallSliding = false;
        }
    }
    void WallJump()
    {

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
