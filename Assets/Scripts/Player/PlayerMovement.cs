using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumping = 16f;
    public bool isFacingRigth = true;

    public bool firstFall = true;

    public GameObject footSteps;
    public AudioSource jumpSound;
    public AudioSource dashSound;

    bool isJumping = false;

    private bool canDash = true;
    public bool isDashing;
    private float dashing = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.5f;

    [SerializeField] private Stamina stamina;
    [SerializeField] private float dashCost = 10;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    [SerializeField] private Animator animator;

    [Header("Online")]
    [SerializeField] private PlayerOnline cs_playerOnline;

    // All players have PlayerOnline.cs. If isNPC --> player can't control it
    public bool isNPC = false;

    private void Start()
    {
        footSteps.SetActive(false);
        animator = GetComponent<Animator>();
        stamina = GetComponent<Stamina>();

        cs_playerOnline = GetComponent<PlayerOnline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstFall)
        {
            animator.SetBool("FirstFall", true);
        }

        if (isDashing)
        {
            return;
        }

        Movement();
        ManageAnimations();

        if (isDashing)
        {
            return;
        }

        Flip();
    }

    public void Movement(string key = default, float key_state = 0, float posX = 0, float posY = 0)
    {
        if (!isNPC)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            cs_playerOnline.ManageOnlineMovement("Horizontal", horizontal, transform.position.x, transform.position.y);
        }
        else
        {
            if (key == "Horizontal")
            {
                if (posX != 0 || posY != 0)
                {
                    transform.position = new Vector3(posX, posY, 0);
                }

                horizontal = key_state;
            }
        }

        if (((Input.GetButtonDown("Jump") && !isNPC) ||
            (key == "Jump" && key_state == 1 && isNPC)) && IsGrounded())
        {
            cs_playerOnline.ManageOnlineMovement("Jump", 1);

            isJumping = true;
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumping);

            animator.SetBool("Jump", true);
        }

        if (((Input.GetButtonUp("Jump") && !isNPC) ||
            (key == "Jump" && key_state == 0) && rb.velocity.y > 0f))
        {
            cs_playerOnline.ManageOnlineMovement("Jump", 0);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        // Dash
        if ((Input.GetButtonDown("Fire3") && !isNPC) ||
            ((key == "Fire3" && key_state == 1) && isNPC) && canDash)
        {
            // TODO QG: Re-Do stamina and UI-related scripts
            if (stamina.UseEnergy(dashCost))
            {
                cs_playerOnline.ManageOnlineMovement("Fire3", 1);
                StartCoroutine(Dash());
            }
        }
    }

    private void ManageAnimations()
    {
        if (isJumping)
        {
            if (rb.velocity.y == 0f && IsGrounded())
            {
                animator.SetBool("Jump", false);
                isJumping = false;
            }
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
            animator.SetBool("FirstFall", false);
            firstFall = false;
        }

        // Animations
        if (!isJumping)
        {
            if (horizontal == 0)
            {
                animator.SetBool("Run", false);
            }
            else
            {
                animator.SetBool("Run", true);
            }
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRigth && horizontal < 0f || !isFacingRigth && horizontal > 0f)
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
