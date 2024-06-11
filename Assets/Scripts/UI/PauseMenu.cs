using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button playBtn, settingsBtn, exitBtn;

    bool isPaused = false;

    [SerializeField] AudioClip buttonSound;
    [SerializeField] GameObject bgmObject;
    AudioSource bgmAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        bgmAudioSource = bgmObject.GetComponent<AudioSource>();

        // Ensure the pause menu is hidden at the start
        pauseMenuUI.SetActive(false);
    }

    private void OnEnable()
    {
        // Add listeners to the buttons
        playBtn.onClick.AddListener(Resume);
        settingsBtn.onClick.AddListener(OpenOptions);
        exitBtn.onClick.AddListener(ExitGame);

    }

    private void OnDisable()
    {
        playBtn.onClick.RemoveAllListeners();
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
        bgmAudioSource.UnPause();
        isPaused = false;
        SoundsManager.instance.PlaySound(buttonSound);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze game time
        bgmAudioSource.Pause();
        isPaused = true;
        SoundsManager.instance.PlaySound(buttonSound);
    }

    void OpenOptions()
    {
        // Open options menu or settings
        SoundsManager.instance.PlaySound(buttonSound);
        Debug.Log("Options button clicked");
    }

    void ExitGame()
    {
        SoundsManager.instance.PlaySound(buttonSound);
        SceneManager.LoadScene("Main menu");
        // Exit to main menu or close the application
        Debug.Log("Exit button clicked");
        //Application.Quit(); // Note: This will not work in the editor
        // Alternatively, load a main menu scene
        // SceneManager.LoadScene("MainMenu");
    }
}
