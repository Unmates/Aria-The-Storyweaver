using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] Vector2 LOSchecksize;
    [SerializeField] Transform LOSCheck;
    [SerializeField] LayerMask LOSLayer;
    [SerializeField] bool onSight;

    EnemyAI enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    private void Update()
    {
        saw();
        if (onSight)
        {
            enemyAI.enabled = true;
        }
        else
        {
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
