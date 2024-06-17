using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject playerObj;
    Health health;

    // Start is called before the first frame update
    void Start()
    {
     health = playerObj.GetComponent<Health>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            health.playerTakeDamage(1);
        }
    }
}
