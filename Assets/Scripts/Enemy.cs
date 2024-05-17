using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform Player;

    [SerializeField] private float maxhp = 100f;
    private float currenthp;

    Animator animator;

    private void Start()
    {
        currenthp = maxhp;
    }

    void Update()
    {

    }

    public void takedamage(float damage)
    {
        currenthp -= damage;
        //if (animator != null)
        //{
        //    animator.SetTrigger("hurt");
        //}
        //else
        //{
        //    Debug.LogWarning("Animator not assigned. Hurt animation not played.");
        //}
        animator.SetTrigger("Hurt");

        if (currenthp < 0)
        {
            dead();
        }
    }

    void dead()
    {
        //if (animator != null)
        //{
        //    animator.SetBool("dead", true);
        //}
        //else
        //{
        //    Debug.LogWarning("Animator not assigned. Dead animation not played.");
        //}
        animator.SetBool("Dead", true);
        Debug.Log("Enemy dead");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
