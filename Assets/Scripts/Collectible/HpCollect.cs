using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpCollect : MonoBehaviour
{
    [SerializeField] float hpValue;
    GameObject playerhp;
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        playerhp = GameObject.Find("Player");
        health = playerhp.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            health.addhp(hpValue);
            gameObject.SetActive(false);
        }
    }
}
