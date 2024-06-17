using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] Vector2 LOSchecksize;
    [SerializeField] Transform LOSCheck;
    [SerializeField] LayerMask LOSLayer;
    public bool onSight;

    [SerializeField] GameObject spriteObj;
    Animator anim;
    [SerializeField] GameObject patrolObj;
    EnemyPatrol enemyPatrol;

    EnemyAI enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        anim = spriteObj.GetComponent<Animator>();
        enemyPatrol = patrolObj.GetComponent<EnemyPatrol>();
        enemyAI = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    private void Update()
    {
        saw();
        if (onSight)
        {
            enemyPatrol.enabled = false;
            enemyAI.enabled = true;
        }
        else
        {
            enemyPatrol.enabled = true;
            enemyAI.enabled = false;
        }
    }

    private bool lineofsight()
    {
        return Physics2D.OverlapBox(LOSCheck.position, LOSchecksize, 0f, LOSLayer);
    }

    void saw()
    {
        if (lineofsight())
        {
            onSight = true;
        }
        else
        {
            onSight = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(LOSCheck.position, LOSchecksize);
    }
}
