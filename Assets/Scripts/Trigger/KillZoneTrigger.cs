using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class KillZoneTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;

    void OnEnable()
    {
        Killzone.OnAllEnemiesKilled += TriggerEvent;
    }

    void OnDisable()
    {
        Killzone.OnAllEnemiesKilled -= TriggerEvent;
    }

    void TriggerEvent()
    {
        Debug.Log("All enemies in the area are killed! Event triggered.");
        playableDirector.Play();
        gameObject.SetActive(false);
    }
}
