using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button playBtn, settingsBtn, exitBtn;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the pause menu is hidden at the start
        pauseMenuUI.SetActive(false);

        // Add listeners to the buttons
        playBtn.onClick.AddListener(Resume);
        settingsBtn.onClick.AddListener(OpenOptions);
        exitBtn.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume game time
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze game time
        isPaused = true;
    }

    void OpenOptions()
    {
        // Open options menu or settings
        Debug.Log("Options button clicked");
    }

    void ExitGame()
    {
        // Exit to main menu or close the application
        Debug.Log("Exit button clicked");
        //Application.Quit(); // Note: This will not work in the editor
        // Alternatively, load a main menu scene
        // SceneManager.LoadScene("MainMenu");
    }
}
