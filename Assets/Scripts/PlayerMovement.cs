using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumping = 16f;
    public bool isFacingRigth = true;

    public GameObject footSteps;

    private bool canDash = true;
    public bool isDashing;
    private float dashing = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown= 0.5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private void Start()
    {
        footSteps.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) 
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && IsGrounded()) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumping);
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f) 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash) 
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private bool IsGrounded() 
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if(rb.velocity.x > 0f || rb.velocity.x < 0f && IsGrounded()) 
        {
            footSteps.SetActive(true);
        }
        
        if(rb.velocity.x == 0f && IsGrounded()) 
        {
            footSteps.SetActive(false);
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if(isFacingRigth && horizontal < 0f || !isFacingRigth && horizontal > 0f) 
        {
            isFacingRigth = !isFacingRigth;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashing, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
