using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    //[SerializeField] GameObject playerObj;
    //[SerializeField] GameObject ramaObj;
    //[SerializeField] GameObject ariaObj;
    //Switch switchClass;

    //Transform ramaTr;
    //Transform ariaTr;

    //public float speed = 200f;
    //public float nextWaypointDistance = 3f;

    //Path path;
    //int currentWaypoint = 0;
    //bool reachedEndOfPath = false;
    //bool isRight = true;

    //Seeker seeker;
    //Rigidbody2D rb;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    switchClass = playerObj.GetComponent<Switch>();
    //    ariaTr = ariaObj.GetComponent<Transform>();
    //    ramaTr = ramaObj.GetComponent<Transform>();
    //    seeker = GetComponent<Seeker>();
    //    rb = GetComponent<Rigidbody2D>();

    //    InvokeRepeating("UpdatePath", 0f, .5f);
    //}

    //void UpdatePath()
    //{
    //    if (switchClass.currentCharacterIndex == 0)
    //    {
    //        if (seeker.IsDone())
    //        {
    //            seeker.StartPath(rb.position, ariaTr.position, OnPathComplete);
    //        }
    //    }
    //    else
    //    {
    //        if (seeker.IsDone())
    //        {
    //            seeker.StartPath(rb.position, ramaTr.position, OnPathComplete);
    //        }
    //    }
    //}

    //void OnPathComplete(Path p)
    //{
    //    if (!p.error)
    //    {
    //        path = p;
    //        currentWaypoint = 0;
    //    }
    //}

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    if (path == null)
    //    {
    //        return;
    //    }
    //    if (currentWaypoint >= path.vectorPath.Count)
    //    {
    //        reachedEndOfPath = true;
    //        return;
    //    }
    //    else
    //    {
    //        reachedEndOfPath = false;
    //    }

    //    Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
    //    Vector2 force = direction * speed * Time.deltaTime;
    //    rb.AddForce(force);
    //    float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

    //    if (distance < nextWaypointDistance)
    //    {
    //        currentWaypoint++;
    //    }
    //    //facing();
    //}

    ////void facing()
    ////{
    ////    if (rb.velocity.x >= 0.01f && isRight == false)
    ////    {
    ////        isRight = true;
    ////        Vector3 localScale = transform.localScale;
    ////        localScale.x *= -1;
    ////        transform.localScale = localScale;
    ////    } else if (rb.velocity.x <= -0.01f && isRight == true)
    ////    {
    ////        isRight = false;
    ////        Vector3 localScale = transform.localScale;
    ////        localScale.x *= -1;
    ////        transform.localScale = localScale;
    ////    }
    ////}

    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject ramaObj;
    [SerializeField] GameObject ariaObj;
    Switch switchClass;

    Transform ramaTr;
    Transform ariaTr;

    public float speed = 5f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        switchClass = playerObj.GetComponent<Switch>();
        ariaTr = ariaObj.GetComponent<Transform>();
        ramaTr = ramaObj.GetComponent<Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (switchClass.currentCharacterIndex == 0)
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, ariaTr.position, OnPathComplete);
            }
        }
        else
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, ramaTr.position, OnPathComplete);
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 movement = new Vector2(direction.x, 0) * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
