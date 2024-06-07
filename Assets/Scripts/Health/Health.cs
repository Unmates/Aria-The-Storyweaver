using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] public float currentPlayerHp;
    Animator animator;

    Aria_ctrl aria_Ctrl;

    // Start is called before the first frame update
    void Awake()
    {
        currentPlayerHp = maxHealth;
        aria_Ctrl = GetComponent<Aria_ctrl>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            aria_Ctrl.hurtanim();
            playerTakeDamage(1);
        }
    }

    public void playerTakeDamage(float _damage)
    {
        currentPlayerHp = Mathf.Clamp(currentPlayerHp - _damage, 0, maxHealth);

        if (currentPlayerHp > 0)
        {
            //playerhurt
        }
        else
        {
            //die
        }
    }
}
