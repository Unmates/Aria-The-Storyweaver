using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rama_ctrl : MonoBehaviour
{
    float hAxis;
    [SerializeField] Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField] float speed = 3f;
    [SerializeField] float jumpPower = 3f;
    [SerializeField] float wallslidespeed = 2f;
    float wallJumpingDirection;
    float wallJumpingTime = 0.2f;
    float wallJumpingCounter;
    [SerializeField] float wallJumpingDuration = 0.4f;
    [SerializeField] float dashingPower = 24f;
    float dashingTime = 0.2f;
    float dashingCooldown = 1f;
    [SerializeField] float atkdmg = 50f;

    [SerializeField] Vector2 groundchecksize;
    [SerializeField] Vector2 wallchecksize;
    [SerializeField] Vector2 atksize;

    Rigidbody2D rb;
    Animator animator;

    [SerializeField] bool onGround = false;
    [SerializeField] bool onWall = false;
    [SerializeField] bool falling;
    bool isFacingRight = true;
    bool isWallJumping;
    bool canDash = true;
    bool isDashing; 

    //[SerializeField] Transform BG;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform atkHitbox;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask enemyLayer;

    //[SerializeField] Lives liveScript;

    [SerializeField] TrailRenderer tr;

    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;

    int attackStep = 0;
    float timeSinceLastAttack = 0f;
    [SerializeField] float attackResetTime = 1f; // Time to reset the attack sequence

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        hAxis = Input.GetAxisRaw("Horizontal");
        jump();
        fallCheck();
        animations();
        wallSlide();
        wallJump();
        Attack();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            //animator.SetBool("Dashing", isDashing);
            StartCoroutine(Dash());
        }

        if (!isWallJumping)
        {
            facing();
        } 
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(hAxis * speed, rb.velocity.y);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundchecksize, 0f, groundLayer);
    }

    void jump()
    {
        if (isGrounded())
        {
            onGround = true;
        }

        else if (!isGrounded())
        {
            onGround = false;
        }

        if (Input.GetButtonDown("Jump") && onGround == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    void fallCheck()
    {
        if (rb.velocity.y < -0.01f)
        {
            falling = true;
        }
        else
        {
            falling = false;
        }
    }

    void facing()
    {
        if (isFacingRight && hAxis < 0f || !isFacingRight && hAxis > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            //BG.localScale = new Vector3(2, 2, 2);
        }
    }

    void animations()
    {
        animator.SetFloat("Moving", Mathf.Abs(hAxis));
        animator.SetBool("onGround", onGround);
        animator.SetBool("Falling", falling);
        animator.SetBool("onWall", onWall);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.tag == "Collectible")
        //{
        //    audioSource.clip = audioClips[0];
        //    audioSource.Play();
        //}

        if (col.tag == "MGround")
        {
            onGround = true;
            this.transform.parent = col.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "MGround")
        {
            onGround = true;
            this.transform.parent = null;
        }
    }

    private bool isWalled()
    {
        return Physics2D.OverlapBox(wallCheck.position, wallchecksize, 0f, wallLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundchecksize);
        Gizmos.DrawWireCube(wallCheck.position, wallchecksize);
        Gizmos.DrawWireCube(atkHitbox.position, atksize);
    }

    private void wallSlide()
    {
        if (isWalled() && hAxis != 0f && onGround == false)
        {
            onWall = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallslidespeed, float.MaxValue));
        }
        else
        {
            onWall = false;
        }
    }

    private void wallJump()
    {
        if (onWall == true)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(stopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localscale = transform.localScale;
                localscale.x *= -1f;
                //BG.localScale = new Vector3(2, 2, 2);
                transform.localScale = localscale;
            }

            Invoke(nameof(stopWallJumping), wallJumpingDuration);
        }
    }

    void stopWallJumping()
    {
        isWallJumping = false;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        animator.SetBool("Dashing", isDashing);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("Dashing", isDashing);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void Attack()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (timeSinceLastAttack >= attackResetTime)
            {
                attackStep = 0;
            }

            SequenceAttack();
            timeSinceLastAttack = 0f;
        }
    }

    void SequenceAttack()
    {
        if (attackStep == 0)
        {
            animator.SetTrigger("Attack1");
            PerformAttack();
            attackStep++;
        }
        else if (attackStep == 1 && onGround)
        {
            animator.SetTrigger("Attack2");
            PerformAttack();
            attackStep++;
        }
        else if (attackStep == 2 && onGround)
        {
            animator.SetTrigger("Attack3");
            PerformAttack();
            attackStep = 0;
        }
        else if (!onGround)
        {
            attackStep = 0;
        }
    }

    void PerformAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(atkHitbox.position, atksize, 0f, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takedamage(atkdmg);
        }
    }

    public void dead()
    {
        animator.SetBool("Dead", true);
        Debug.Log("Player dead");
        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;
    }
}
