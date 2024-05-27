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
        animator.SetTrigger("Hurt");

        if (currenthp < 0)
        {
            dead();
        }
    }

    void dead()
    {
        animator.SetBool("Dead", true);
        Debug.Log("Enemy dead");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
