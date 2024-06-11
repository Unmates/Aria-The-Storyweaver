using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The target the camera should follow
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public float minX; // Minimum X boundary
    public float maxX; // Maximum X boundary
    public float minY; // Minimum Y boundary
    public float maxY; // Maximum Y boundary

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = transform.position.z;  // Keep the camera's original Z position

            // Get the camera's view dimensions
            float halfHeight = cam.orthographicSize;
            float halfWidth = cam.aspect * halfHeight;

            // Clamp the desired position to ensure it stays within the level bounds
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX + halfWidth, maxX - halfWidth);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY + halfHeight, maxY - halfHeight);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
