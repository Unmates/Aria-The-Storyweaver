using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] GameObject dialogObj;
    Dialog dialogScript;  // Reference to the Dialog script

    private void Start()
    {
        dialogScript = dialogObj.GetComponent<Dialog>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogScript.StartDialog();
            gameObject.SetActive(false);
        }
    }
}
