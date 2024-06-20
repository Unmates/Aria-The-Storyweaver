using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutroialTrigger : MonoBehaviour
{
    [SerializeField] GameObject objectToShow;
    [SerializeField] float moveDistance = 0.5f; // Adjust this value to control how much the object moves
    [SerializeField] float moveSpeed = 2f; // Speed of the movement

    private bool isPlayerInside = false;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    void Start()
    {
        initialPosition = objectToShow.transform.position;
        targetPosition = initialPosition + Vector3.up * moveDistance;
        objectToShow.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInside)
        {
            objectToShow.transform.position = Vector3.MoveTowards(objectToShow.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            objectToShow.transform.position = Vector3.MoveTowards(objectToShow.transform.position, initialPosition, moveSpeed * Time.deltaTime);
            if (objectToShow.transform.position == initialPosition)
            {
                objectToShow.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            objectToShow.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
