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
    public AudioSource jumpSound;
    public AudioSource dashSound;

    private bool canDash = true;
    public bool isDashing;
    private float dashing = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown= 0.5f;

    [SerializeField] private Stamina stamina;
    [SerializeField] private float dashCost = 10;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    [SerializeField] private Animator animator;

    private void Start()
    {
        footSteps.SetActive(false);
        animator = GetComponent<Animator>();
        stamina = GetComponent<Stamina>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) 
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded()) 
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumping);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetButtonDown("Fire3") && canDash) 
        {
            if (stamina.UseEnergy(dashCost))
            {
                StartCoroutine(Dash());
            }
        }

        if (isDashing)
        {
            return;
        }

        if (rb.velocity.x > 0f || rb.velocity.x < 0f)
        {
            if (IsGrounded() && rb.velocity.y == 0f)
            {
                footSteps.SetActive(true);
            }
        }

        if (rb.velocity.x == 0f && IsGrounded())
        {
            footSteps.SetActive(false);
        }

        // Animations
        if (horizontal == 0)
        {
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Run", true);
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        Flip();
    }

    public bool IsGrounded() 
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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
        dashSound.Play();
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
