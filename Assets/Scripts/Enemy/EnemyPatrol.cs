using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Transform left, right;
    [SerializeField] Transform enemy;
    [SerializeField] float speed;
    Vector3 initscale;
    bool movingLeft;

    // Start is called before the first frame update
    void Awake()
    {
        initscale = transform.localScale;
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
            if (enemy.position.x <= left.position.x)
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
        movingLeft = !movingLeft;
    }

    void MoveInDirection(int _direction)
    {
        enemy.localScale = new Vector3(Mathf.Abs(initscale.x) * _direction, initscale.y, initscale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
}
