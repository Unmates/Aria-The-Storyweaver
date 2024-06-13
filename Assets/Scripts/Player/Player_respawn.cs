using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_respawn : MonoBehaviour
{
    public Transform currentCheckpoint;
    Health health;

    [Header("Player object")]
    [SerializeField] GameObject aria_obj;
    Transform aria_tr;

    [SerializeField] GameObject rama_obj;
    Transform rama_tr;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();

        aria_tr = aria_obj.GetComponent<Transform>();
        rama_tr = rama_obj.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        aria_tr.position = currentCheckpoint.position;
        rama_tr.position = currentCheckpoint.position;
        health.Respawn();
    }
}
