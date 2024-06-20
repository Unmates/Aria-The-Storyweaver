using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentPlayerHp;

    [Header("Respawn")]
    public Transform currentCheckpoint;
    Player_respawn player_Respawn;

    [Header("iFrames")]
    [SerializeField] float invulDur;
    [SerializeField] int numberOfFlashes;
    bool isInvul = false;
    SpriteRenderer aria_sprite;
    SpriteRenderer rama_sprite;

    [Header ("Player object")]
    Switch switchClass;

    [SerializeField] GameObject aria_obj;
    Animator aria_anim;
    Aria_ctrl aria_Ctrl;
    Transform aria_tr;

    [SerializeField] GameObject rama_obj;
    Animator rama_anim;
    Rama_ctrl rama_Ctrl;
    Transform rama_tr;

    [Header("Audio")]
    [SerializeField] AudioClip hurtSound;
    [SerializeField] AudioClip deadSound;

    [Header("UI")]
    [SerializeField] GameObject inGameUI;
    PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Awake()
    {
        switchClass = GetComponent<Switch>();
        currentPlayerHp = maxHealth;
        player_Respawn = GetComponent<Player_respawn>();
        aria_Ctrl = aria_obj.GetComponent<Aria_ctrl>();
        aria_anim = aria_obj.GetComponent<Animator>();
        aria_sprite = aria_obj.GetComponent<SpriteRenderer>();
        aria_tr = aria_obj.GetComponent<Transform>();

        rama_Ctrl = rama_obj.GetComponent<Rama_ctrl>();
        rama_anim = rama_obj.GetComponent<Animator>();
        rama_sprite = rama_obj.GetComponent<SpriteRenderer>();
        rama_tr = rama_obj.GetComponent<Transform>();

        pauseMenu = inGameUI.GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (switchClass.currentCharacterIndex == 1)
        {
            if (rama_Ctrl.isDashing)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && rama_Ctrl.canDash)
            {
                StartCoroutine(rama_Ctrl.Dash());
            }
        }
    }

    public void playerTakeDamage(float _damage)
    { 
        if (!isInvul)
        {
            int currenChar = switchClass.currentCharacterIndex;

            if (currenChar == 0)
            {
                currentPlayerHp = Mathf.Clamp(currentPlayerHp - _damage, 0, maxHealth);
            }
            else
            {
                currentPlayerHp = Mathf.Clamp(currentPlayerHp - (_damage * 0.5f), 0, maxHealth);
            }

            if (currentPlayerHp > 0)
            {
                SoundsManager.instance.PlaySound(hurtSound);
                if (currenChar == 0)
                {
                    aria_anim.SetTrigger("Hurt");
                }
                else
                {
                    rama_anim.SetTrigger("Hurt");
                }
                StartCoroutine(Invul());
            }
            else
            {
                SoundsManager.instance.PlaySound(deadSound);
                if (currenChar == 0)
                {
                    aria_anim.SetTrigger("Hurt");
                    aria_Ctrl.dead();
                }
                else
                {
                    rama_anim.SetTrigger("Hurt");
                    rama_Ctrl.Dead();
                }
                StartCoroutine(gameOverDelay());
            }
        }
    }

    public void addhp(float _value)
    {
        currentPlayerHp = Mathf.Clamp(currentPlayerHp + _value, 0, maxHealth);
    }

    public void Respawn()
    {
        aria_tr.position = currentCheckpoint.position;
        rama_tr.position = currentCheckpoint.position;
        currentPlayerHp = maxHealth;
        aria_anim.SetBool("Dead", false);
        rama_anim.SetBool("Dead", false);
    }

    IEnumerator Invul()
    {
        isInvul = true;
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
        isInvul = false;
    }

    IEnumerator gameOverDelay()
    {
        yield return new WaitForSeconds(1);
        pauseMenu.GameOverScreen();
    }

    public void EnableControll()
    {
        aria_Ctrl.enabled = true;
        rama_Ctrl.enabled = true;
        switchClass.enabled = true;
    }

    public void DisableControll()
    {
        if (switchClass.currentCharacterIndex == 0)
        {
            aria_Ctrl.StopMovement();
            aria_Ctrl.enabled = false;
        }
        else
        {
            rama_Ctrl.StopMovement();
            rama_Ctrl.enabled = false;
        }
        switchClass.enabled = false;
    }
}
