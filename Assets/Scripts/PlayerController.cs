﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public float jumpForce;
    public float moveInput;
    private Rigidbody2D rb;

    private SpriteRenderer sp;

    public float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;

    bool wallJumping;
    public float wallJumpTime ;
    private float originalWallJumpTime;
    private float wallJumpSpeed;
    public Vector2 wJForce = new Vector2(); //new & unused

    bool wallSliding;
    public float wallSlidingSpeed;

    //Collision detection
    private bool grounded;
    private bool ceilingCheck;
    private bool leftWalled;
    private bool rightWalled;

    public float distanceDetection;
    Vector2 size;
    public LayerMask maskGround;

    public bool magnestism; //delete if not used

    void Awake()
    {
        size = new Vector2(GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.y);
        originalWallJumpTime = wallJumpTime;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        magnestism = GetComponent<Magnetism>().magnestism; //delete if not used
        moveInput = Input.GetAxisRaw("Horizontal");

        Detection();
        Movement();
    }

    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("Grounded", grounded);
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
        Vector2 upRayL =    (Vector2)transform.position + Vector2.up * size * 0.5f + Vector2.left * size * 0.5f;   //up
        Vector2 upRayR =    (Vector2)transform.position + Vector2.up * size * 0.5f + Vector2.right * size * 0.5f;

        grounded = (Physics2D.Raycast(downRayL, Vector2.down, distanceDetection, maskGround) || Physics2D.Raycast(downRayR, Vector2.down, distanceDetection, maskGround));
        leftWalled = (Physics2D.Raycast(leftRayU, Vector2.left, distanceDetection, maskGround) || Physics2D.Raycast(leftRayD, Vector2.left, distanceDetection, maskGround));
        rightWalled = (Physics2D.Raycast(rightRayU, Vector2.right, distanceDetection, maskGround) || Physics2D.Raycast(rightRayD, Vector2.right, distanceDetection, maskGround));
        ceilingCheck = (Physics2D.Raycast(upRayL, Vector2.up, distanceDetection, maskGround) || Physics2D.Raycast(upRayR, Vector2.up, distanceDetection, maskGround));
    }
    void Movement()
    {
        if (!wallJumping)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        if (sp.flipX == false && rb.velocity.x < 0 && moveInput <=0)
        {
            Flip();
        }
        else if (sp.flipX == true && rb.velocity.x > 0 && moveInput>=0)
        {
            Flip();
        }
        else if(grounded && sp.flipX == true && rb.velocity.x >0)
        {
            Flip();
        }
        else if (grounded && sp.flipX == false && rb.velocity.x < 0)
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
        if (ceilingCheck)
        {
            jumpTimeCounter = 0;
            wallJumpTime = 0;
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
        if (wallJumpTime <= 0 || (grounded) || (leftWalled && wallJumpTime < originalWallJumpTime - 0.1f) || (rightWalled && wallJumpTime < originalWallJumpTime - 0.1f))
        {
            wallJumping = false;
            wallJumpTime = originalWallJumpTime;
        }
        if (leftWalled && !grounded && Input.GetButtonDown("Jump"))
        {
            wallJumping = true;
            rb.velocity = new Vector2(wJForce.x, wJForce.y);
        }
        if (rightWalled && !grounded && Input.GetButtonDown("Jump"))
        {
            wallJumping = true;
            rb.velocity = new Vector2(-wJForce.x, wJForce.y);
        }
        if (wallJumping)
        {
            wallJumpTime -= Time.deltaTime;
            wallJumpSpeed = Mathf.Pow(1.0f - (wallJumpTime / originalWallJumpTime), 1) * (speed*0.05f);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (moveInput * wallJumpSpeed)*Time.deltaTime, -speed, speed), rb.velocity.y);
        }
    }
    void Flip()
    {
        sp.flipX = !sp.flipX;
    }
}
