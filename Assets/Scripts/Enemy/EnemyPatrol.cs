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
    Vector3 spriteScale;
    [SerializeField] bool movingLeft;
    [SerializeField] GameObject spriteObj;
    Transform spriteTr;
    Animator animator;
    [SerializeField] float idleDuration;
    float idleTimer;

    // Start is called before the first frame update
    void Awake()
    {
        initscale = enemy.localScale;
        animator = spriteObj.GetComponent<Animator>();
        spriteTr = spriteObj.gameObject.GetComponent<Transform>();
        spriteScale = spriteTr.localScale;
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
        spriteTr.localScale = new Vector3(Mathf.Abs(spriteScale.x) * _direction, spriteScale.y, spriteScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
}
