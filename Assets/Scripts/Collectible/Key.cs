using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviour
{
    public class Key : MonoBehaviour
    {
        public Door door; // Reference to the door to be opened

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                door.OpenDoor();
                Destroy(gameObject); // Destroy the key after it's picked up
            }
        }
    }
}
