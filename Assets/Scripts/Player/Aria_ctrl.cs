using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aria_ctrl : MonoBehaviour
{
    float hAxis;

    [SerializeField] float maxHP = 200f;
    float HP;
    [SerializeField] float speed = 3f;
    [SerializeField] float jumpPower = 3f;
    [SerializeField] float airjumpCount = 1f;
    [SerializeField] float maxairjump = 1f;
    [SerializeField] float atkdmg = 50f;

    [SerializeField] Vector2 groundchecksize;
    [SerializeField] Vector2 atksize;

    Rigidbody2D rb;
    Animator animator;
    Health health;

    [SerializeField] bool onGround = false;
    [SerializeField] bool falling;
    bool isFacingRight = true;

    //[SerializeField] Transform BG;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform atkHitbox;
    [SerializeField] Transform firePoint;

    [SerializeField] GameObject healthobj;
    [SerializeField] GameObject[] fireballs;
    [SerializeField] GameObject fireballPrefab;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;

    //[SerializeField] Lives liveScript;

    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        health = healthobj.GetComponent<Health>();
        HP = maxHP;
        SpawnFireball();
    }

    void SpawnFireball()
    {
        for (int i = 0; i < 10; i++)
        {
            fireballs[i] = Instantiate(fireballPrefab);
            fireballs[i].SetActive(false);
        }
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
            airjumpCount = maxairjump;
        }

        else if (!isGrounded())
        {
            onGround = false;
        }

        if (Input.GetButtonDown("Jump") && onGround == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        else if (Input.GetButtonDown("Jump") && onGround == false && airjumpCount > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower * 0.8f);
            airjumpCount -= 1f;
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
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            health.playerTakeDamage(1);
            Debug.Log("Player hit and damage applied.");
        }

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundchecksize);
        Gizmos.DrawWireCube(atkHitbox.position, atksize);
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(atkHitbox.position, atksize, 0f, enemyLayer);

            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().takedamage(atkdmg);
            }

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

    public void dead()
    {
        animator.SetBool("Dead", true);
        Debug.Log("Player dead");
        //GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
