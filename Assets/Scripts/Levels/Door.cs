using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Vector3 openPosition;
    [SerializeField] float openSpeed = 2f;
    private Vector3 closedPosition;

    private bool isOpen = false;

    void Start()
    {
        closedPosition = transform.position; // Save the initial closed position of the door
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            StartCoroutine(Open());
        }
    }

    private IEnumerator Open()
    {
        isOpen = true;

        while (Vector3.Distance(transform.position, openPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, openSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = openPosition;
    }
}
