using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject doorObj;
    Door door;
    [SerializeField] AudioClip collectSound;

    private void Start()
    {
        door = doorObj.GetComponent<Door>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundsManager.instance.PlaySound(collectSound);
            door.OpenDoor();
            Destroy(gameObject);
        }
    }
}