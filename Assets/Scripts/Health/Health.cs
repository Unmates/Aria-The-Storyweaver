using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float maxHealth;
    [SerializeField] public float currentPlayerHp;

    [Header("iFrames")]
    [SerializeField] float invulDur;
    [SerializeField] int numberOfFlashes;
    SpriteRenderer spriteRend;

    [Header ("Player object")]
    [SerializeField] GameObject aria_obj;
    Animator aria_anim;
    Aria_ctrl aria_Ctrl;

    [SerializeField] GameObject rama_obj;
    Animator rama_anim;
    Rama_ctrl rama_Ctrl;

    // Start is called before the first frame update
    void Awake()
    {
        currentPlayerHp = maxHealth;
        aria_Ctrl = aria_obj.GetComponent<Aria_ctrl>();
        aria_anim = aria_obj.GetComponent<Animator>();

        rama_Ctrl = rama_obj.GetComponent<Rama_ctrl>();
        rama_anim = rama_obj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playerTakeDamage(float _damage)
    { 
        currentPlayerHp = Mathf.Clamp(currentPlayerHp - _damage, 0, maxHealth);

        if (currentPlayerHp > 0)
        {
            aria_anim.SetTrigger("Hurt");
            rama_anim.SetTrigger("Hurt");
        }
        else
        {
            aria_Ctrl.dead();
            rama_Ctrl.dead();
        }
    }

    public void addhp(float _value)
    {
        currentPlayerHp = Mathf.Clamp(currentPlayerHp + _value, 0, maxHealth);
    }
}
