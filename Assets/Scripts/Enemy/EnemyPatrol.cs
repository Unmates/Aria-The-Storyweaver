using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Transform left; 
    [SerializeField] Transform right;
    [SerializeField] Transform enemy;
    [SerializeField] float speed;
    Vector3 initscale;
    [SerializeField] bool movingLeft;
    [SerializeField] Animator animator;
    [SerializeField] float idleDuration;
    float idleTimer;

    // Start is called before the first frame update
    void Awake()
    {
        initscale = enemy.localScale;
        animator = enemy.GetComponent<Animator>();
    }

    private void OnDisable()
    {
        animator.SetBool("moving", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= left.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= right.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    void DirectionChange()
    {
        animator.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        animator.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(initscale.x) * _direction, initscale.y, initscale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
}
