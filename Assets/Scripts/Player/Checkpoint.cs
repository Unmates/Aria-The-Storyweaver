using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject respawnObj;
    Health player_Respawn;
    Animator animator;
    [SerializeField] AudioClip activeSound;

    // Start is called before the first frame update
    void Start()
    {
        player_Respawn = respawnObj.GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundsManager.instance.PlaySound(activeSound);
            animator.SetBool("Active", true);
            player_Respawn.currentCheckpoint = transform;
        }
    }
}
