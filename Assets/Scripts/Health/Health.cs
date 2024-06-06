using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] public float currentPlayerHp;

    // Start is called before the first frame update
    void Awake()
    {
        currentPlayerHp = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerTakeDamage(1);
        }
    }

    void playerTakeDamage(float _damage)
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
