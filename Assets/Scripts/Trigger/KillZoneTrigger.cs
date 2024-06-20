using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class KillZoneTrigger : MonoBehaviour
{
    [SerializeField] float targetEnemyDead = 2f;
    public float enemyDead;
    [SerializeField] PlayableDirector playableDirector;

    //public delegate void AllEnemiesKilledEventHandler();
    //public event AllEnemiesKilledEventHandler OnAllEnemiesKilled;

    //private void Start()
    //{
    //    OnAllEnemiesKilled += TriggerEvent;
    //}

    public void EnemyKilled()
    {
        enemyDead++;
        if (enemyDead >= targetEnemyDead)
        {
            TriggerEvent();
        }
    }

    private void TriggerEvent()
    {
        Debug.Log("All enemies in the area are killed! Event triggered.");
        playableDirector.Play();
        gameObject.SetActive(false);
    }
}
