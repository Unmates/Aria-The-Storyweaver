using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    public delegate void KillZoneEventHandler();
    public static event KillZoneEventHandler OnAllEnemiesKilled;

    private int enemiesInZone = 0;
    private int enemiesKilled = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInZone++;
            other.GetComponent<Enemy>().OnEnemyDeath += EnemyKilled;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInZone--;
            other.GetComponent<Enemy>().OnEnemyDeath -= EnemyKilled;
        }
    }

    void EnemyKilled()
    {
        enemiesKilled++;
        if (enemiesKilled >= enemiesInZone)
        {
            if (OnAllEnemiesKilled != null)
            {
                OnAllEnemiesKilled.Invoke();
            }
        }
    }
}
