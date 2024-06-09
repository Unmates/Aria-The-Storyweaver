using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button start_btn, setting_btn, exit_btn;

    private void OnEnable()
    {
        start_btn.onClick.AddListener(StartGame);

    }

    private void OnDisable()
    {
        start_btn.onClick.RemoveAllListeners();
    }

    void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
