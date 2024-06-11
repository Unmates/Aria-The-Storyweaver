using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_respawn : MonoBehaviour
{
    [SerializeField] AudioClip activeSound;
    Transform currentCheckpoint;
    Health health;
    float maxhp;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        maxhp = health.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Respawn()
    {
        transform.position = currentCheckpoint.position;
        health.currentPlayerHp = maxhp;
    }
}
