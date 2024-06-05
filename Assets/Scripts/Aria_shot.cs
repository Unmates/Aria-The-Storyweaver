using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aria_shot : MonoBehaviour
{
    [SerializeField] float atkdmg = 50f;
    [SerializeField] float speed = 3f;
    float direction;
    float lifetime;

    bool hit;

    Animator animator;
    BoxCollider2D boxCollider2d;
    Enemy enemy;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) { return; }
        float moveSpeed = speed * Time.deltaTime * direction;
        transform.Translate(0, -moveSpeed, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5f)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.takedamage(atkdmg);
                Debug.Log("Enemy hit and damage applied.");
            }
            else
            {
                Debug.LogWarning("No Enemy component found on the collided object with tag 'Enemy'.");
            }
        }
        else if (col.tag == "Player")
        {
            return;
        }
        hit = true;
        animator.SetTrigger("Explode");
        boxCollider2d.enabled = false;
    }

    public void setDirection(float _direction)
    {
        lifetime = 0;
        direction=_direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider2d.enabled = true;

        float localScaleY = transform.localScale.y;
        if (Mathf.Sign(localScaleY) != _direction)
        {
            localScaleY = -localScaleY;
        }

        transform.localScale = new Vector3(transform.localScale.x, localScaleY, transform.localScale.z);
    }

    void deactivate()
    {
        gameObject.SetActive(false);
    }
}
