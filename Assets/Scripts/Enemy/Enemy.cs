using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxhp = 100f;
    [SerializeField] float damage;
    float currenthp;

    [SerializeField] float attackRange = 1.5f; // Adjustable attack distance
    [SerializeField] float attackRate = 1.0f; // Adjustable attack speed (attacks per second)
    float nextAttackTime = 0f;  // Timer for attack cooldown

    public Animator animator;
    public Transform attackPoint; // Optional: Reference to the enemy's attack point

    [SerializeField] GameObject playerhp;
    Health health;

    private void Start()
    {
        currenthp = maxhp;
        animator = GetComponent<Animator>();
        health = playerhp.GetComponent<Health>();
    }

    void Update()
    {
        //// Check if player is within attack range
        //Transform player = FindObjectOfType<Player>().transform; // Assuming a Player script exists
        //float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //if (distanceToPlayer <= attackRange)
        //{
        //    // If player is in range and attack cooldown is ready, attack
        //    if (Time.time >= nextAttackTime)
        //    {
        //        AttackPlayer();
        //        nextAttackTime = Time.time + 1f / attackRate; // Reset attack cooldown timer
        //    }

        //    // Optional: Face the player while attacking (consider using Lerp for smooth turning)
        //    transform.rotation = Quaternion.LookRotation(player.position - transform.position);
        //}
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
        this.enabled = false;
    }

    void AttackPlayer()
    {
        animator.SetTrigger("attack"); // Trigger attack animation

        // Handle attack logic (consider using a SphereCast for more precise collision detection):
        if (attackPoint != null) // Check if attackPoint is assigned (optional)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
            foreach (Collider2D collider in hitColliders)
            {
                if (collider.gameObject.tag == "Player") // Replace "Player" with your player's tag
                {
                    //collider.gameObject.GetComponent<Aria_ctrl>().playertakedamage(damage); // Assuming a Player script with TakeDamage method
                    break; // Stop after damaging the Player
                }
            }
        }
        else
        {
            // Handle attack collision without attackPoint (e.g., apply damage to Player directly)
            // ... (replace with your preferred attack collision handling)
        }
    }
}
