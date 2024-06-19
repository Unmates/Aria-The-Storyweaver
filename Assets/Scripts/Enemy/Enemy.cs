using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] float maxhp = 100f;
    [SerializeField] float damage;
    float currenthp;

    [SerializeField] float attackRate;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] float range;
    [SerializeField] float colliderDistance;
    float cooldownTimer = Mathf.Infinity;
    Rigidbody2D rb;

    public Animator animator;

    [Header("Player detect")]
    [SerializeField] GameObject playerhp;
    Health health;

    [SerializeField] LayerMask playerLayer;

    EnemyPatrol enemyPatrol;
    [SerializeField] GameObject enemyAI;
    LineOfSight lineOfSight;
    EnemyAI enemyAIfunc;
    bool isAttacking = false;

    [Header("Sound")]
    [SerializeField] AudioClip attackSound;

    private void Start()
    {
        currenthp = maxhp;
        animator = GetComponent<Animator>();
        health = playerhp.GetComponent<Health>();
        enemyAIfunc = enemyAI.GetComponent<EnemyAI>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        lineOfSight = GetComponentInParent<LineOfSight>();

        rb = enemyAI.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (playerClose())
        {
            if (cooldownTimer >= attackRate)
            {
                cooldownTimer = 0;
                animator.SetTrigger("attack");
                isAttacking = true;
            }
        }
        PatrolController();
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            health.playerTakeDamage(damage);
        }
    }

    public void takedamage(float damage)
    {
        currenthp -= damage;
        animator.SetTrigger("hurt");

        if (currenthp <= 0)
        {
            dead();
        }
    }

    void dead()
    {
        animator.SetBool("dead", true);
        Debug.Log("Enemy dead");
        GetComponent<Collider2D>().enabled = false;
        enemyPatrol.enabled = false;
        enemyAIfunc.enabled = false;
        lineOfSight.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        this.enabled = false;
    }

    bool playerClose()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0 , Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    void DamagePlayer()
    {
        SoundsManager.instance.PlaySound(attackSound);
        if (playerClose())
        {
            health.playerTakeDamage(damage);
        }
    }

    void PatrolController()
    {
        if (isAttacking == true)
        {
            animator.SetBool("moving", false);
            enemyPatrol.enabled = false;
            lineOfSight.onSight = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            animator.SetBool("moving", true);
            enemyPatrol.enabled = true;
            lineOfSight.onSight = true;
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void DisableAttack()
    {
        isAttacking = false;
    }
}
