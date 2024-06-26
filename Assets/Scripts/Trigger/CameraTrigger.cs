using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playableDirector.Play();
            gameObject.SetActive(false);
        }
    }
}
