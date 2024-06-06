using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    [SerializeField] Image totalHealthBar;
    [SerializeField] Image currentHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentPlayerHp / 10;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentPlayerHp / 10;
    }
}
