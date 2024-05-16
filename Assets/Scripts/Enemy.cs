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
    // Update is called once per frame
    void Update()
    {
        ////if player left look left
        //if (Player.position.x < transform.position.x)
        //{
        //    Vector3 enemyscale = transform.localScale;
        //    enemyscale.x *= -1;
        //    transform.localScale = enemyscale;
        //}
        ////if player right look right
        //if (Player.position.x > transform.position.x)
        //{
        //    Vector3 enemyscale = transform.localScale;
        //    enemyscale.x *= -1;
        //    transform.localScale = enemyscale;
        //}
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
