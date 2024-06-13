using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aria_ctrl : MonoBehaviour
{
    float hAxis;

    [SerializeField] float speed = 3f;
    [SerializeField] float jumpPower = 3f;
    [SerializeField] float airjumpCount = 1f;
    [SerializeField] float maxairjump = 1f;

    [SerializeField] Vector2 groundchecksize;

    Rigidbody2D rb;
    Animator animator;
    Health health;

    [SerializeField] bool onGround = false;
    [SerializeField] bool falling;
    bool isDoubleJump;
    bool isFacingRight = true;

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform firePoint;

    [SerializeField] GameObject healthobj;
    [SerializeField] GameObject[] fireballs;
    [SerializeField] GameObject fireballPrefab;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;

    [Header("Audio")]
    [SerializeField] AudioClip fireballsSound;
    [SerializeField] AudioClip jumpSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = healthobj.GetComponent<Health>();
        SpawnFireball();
    }

    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        jump();
        fallCheck();
        animations();
        Attack();
        facing();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(hAxis * speed, rb.velocity.y);
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
            isDoubleJump = false;
            airjumpCount = maxairjump;
        }

        else if (!isGrounded())
        {
            onGround = false;
        }

        if (Input.GetButtonDown("Jump") && onGround == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            SoundsManager.instance.PlaySound(jumpSound);
        }

        else if (Input.GetButtonDown("Jump") && onGround == false && airjumpCount > 0f)
        {
            isDoubleJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower * 0.8f);
            airjumpCount -= 1f;
            SoundsManager.instance.PlaySound(jumpSound);
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
        }
    }

    void animations()
    {
        animator.SetFloat("Moving", Mathf.Abs(hAxis));
        animator.SetBool("onGround", onGround);
        animator.SetBool("Falling", falling);
        animator.SetBool("Double", isDoubleJump);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            health.playerTakeDamage(0);
            Debug.Log("Player hit and damage applied.");
        }

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundchecksize);
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SoundsManager.instance.PlaySound(fireballsSound);
            animator.SetTrigger("Attack");

            fireballs[findFireball()].transform.position = firePoint.position;
            fireballs[findFireball()].GetComponent<Aria_shot>().setDirection(Mathf.Sign(transform.localScale.x));
        }
    }

    int findFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    void SpawnFireball()
    {
        for (int i = 0; i < 10; i++)
        {
            fireballs[i] = Instantiate(fireballPrefab);
            fireballs[i].SetActive(false);
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
