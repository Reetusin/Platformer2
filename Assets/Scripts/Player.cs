using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("VFX")]
    public GameObject bloodVfx;

    [Header("Movement")]
    public float movementSpeed = 10.0f;
    public float jumpHeight = 3;
    public float dashSpeed = 20;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1.0f;

    
    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform groundCheck; //player legs
    public float radius = 0.2f;

    [Header("Jump Mechanics")]
    public float coyoteTime = 0.2f;
    public float jumBufferTime = 0.2f;
    public int maxJumps = 2;


    private int jumpsLeft;
    private float jumpBufferCounter;
    private float coyoteCounter;
    private bool isGrounded;
    private Rigidbody2D rb;
    public float inputX;
    public bool isDashing;
    public float dashTime;
    public float dashCooldownTime;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        
        //OverlapCircle - checks circle area for ground objects
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);

        if(isGrounded)
        {
            coyoteCounter = coyoteTime;
            jumpsLeft = maxJumps;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        
        if ((coyoteCounter > 0 || jumpsLeft > 0) && jumpBufferCounter > 0)
        {
            jumpBufferCounter = 0;

            var jumpForce = Mathf.Sqrt(-2 * Physics2D.gravity.y * jumpHeight * rb.gravityScale);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            if(!isGrounded)
            {
                jumpsLeft--;
            }
        }

        //dasdh
        if(Input.GetButtonDown("Fire3") && dashCooldownTime <= 0)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashCooldownTime = dashCooldown;
        }

        if(isDashing)
        {
            rb.velocity = new Vector2(dashSpeed * inputX, rb.velocity.y);
            dashTime -= Time.deltaTime;

            if(dashTime <= 0)
            {
                isDashing = false;
            }
        }
        dashCooldownTime -= Time.deltaTime;
    }   

    void FixedUpdate() //physics update
    {
        if(!isDashing)
            rb.velocity = new Vector2(inputX * movementSpeed, rb.velocity.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (groundCheck != null)
        {
            Gizmos.DrawWireSphere(groundCheck.position, radius);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.relativeVelocity.magnitude > 25)
        {
            Instantiate(bloodVfx, transform.position, Quaternion.identity);
        }
    }
}
