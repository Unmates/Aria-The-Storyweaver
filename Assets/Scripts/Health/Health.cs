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
    SpriteRenderer aria_sprite;
    SpriteRenderer rama_sprite;

    [Header ("Player object")]
    [SerializeField] GameObject aria_obj;
    Animator aria_anim;
    Aria_ctrl aria_Ctrl;

    [SerializeField] GameObject rama_obj;
    Animator rama_anim;
    Rama_ctrl rama_Ctrl;

    [Header("Audio")]
    [SerializeField] AudioClip hurtSound;
    [SerializeField] AudioClip deadSound;

    // Start is called before the first frame update
    void Awake()
    {
        currentPlayerHp = maxHealth;
        aria_Ctrl = aria_obj.GetComponent<Aria_ctrl>();
        aria_anim = aria_obj.GetComponent<Animator>();
        aria_sprite = aria_obj.GetComponent<SpriteRenderer>();


        rama_Ctrl = rama_obj.GetComponent<Rama_ctrl>();
        rama_anim = rama_obj.GetComponent<Animator>();
        rama_sprite = rama_obj.GetComponent<SpriteRenderer>();
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
            SoundsManager.instance.PlaySound(hurtSound);
            aria_anim.SetTrigger("Hurt");
            rama_anim.SetTrigger("Hurt");
            StartCoroutine(Invul());
        }
        else
        {
            SoundsManager.instance.PlaySound(deadSound);
            aria_Ctrl.dead();
            rama_Ctrl.dead();
        }
    }

    public void addhp(float _value)
    {
        currentPlayerHp = Mathf.Clamp(currentPlayerHp + _value, 0, maxHealth);
    }

    IEnumerator Invul()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        //invul dura
        for (int i = 0; i < numberOfFlashes; i++)
        {
            aria_sprite.color = new Color(1, 0, 0, 0.5f);
            rama_sprite.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(invulDur / (numberOfFlashes * 2));
            aria_sprite.color = Color.white;
            rama_sprite.color = Color.white;
            yield return new WaitForSeconds(invulDur / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, true);
    }
}
