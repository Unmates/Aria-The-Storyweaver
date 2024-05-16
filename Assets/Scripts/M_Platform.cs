using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class M_Platform : MonoBehaviour
{
    public Transform platform;
    public Transform startplat;
    public Transform endplat;
    [SerializeField]
    public float speed = 1.0f;

    int direction = 1;

    private void OnDrawGizmos()
    {
        if (platform != null && startplat != null && endplat != null)
        {
            Gizmos.DrawLine(platform.transform.position, startplat.position);
            Gizmos.DrawLine(platform.transform.position, endplat.position);
        }
    }
    
    Vector2 currentmovementtarget()
    {
        if (direction == 1)
        {
            return startplat.position;
        }
        else
        {
            return endplat.position;
        }
    }

    void Update()
    {
        Vector2 target = currentmovementtarget();

        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)platform.position).magnitude;

        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }
}
