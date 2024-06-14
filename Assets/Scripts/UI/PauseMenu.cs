using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    SettingMenu settingMenu;
    [SerializeField] GameObject ariaFace;
    [SerializeField] GameObject ramaFace;
    [SerializeField] GameObject blackBg;
    public Button playBtn, settingsBtn, exitBtn, retryBtn, giveUpBtn;

    bool isPaused = false;

    [Header("Audio")]
    [SerializeField] AudioClip buttonSound;
    [SerializeField] GameObject bgmObject;
    AudioSource bgmAudioSource;

    [Header("Others")]
    [SerializeField] GameObject playerObj;
    Switch switchClass;
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        settingMenu = GetComponent<SettingMenu>();
        health = playerObj.GetComponent<Health>();
        switchClass = playerObj.GetComponent<Switch>();
        bgmAudioSource = bgmObject.GetComponent<AudioSource>();

        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        blackBg.SetActive(false);
    }

    private void OnEnable()
    {
        // Add listeners to the buttons
        playBtn.onClick.AddListener(Resume);
        settingsBtn.onClick.AddListener(OpenOptions);
        exitBtn.onClick.AddListener(ExitGame);
        retryBtn.onClick.AddListener(Retry);
        giveUpBtn.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        playBtn.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        faceCheck();

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
        Time.timeScale = 1f;
        bgmAudioSource.UnPause();
        isPaused = false;
        SoundsManager.instance.PlaySound(buttonSound);
        blackBg.SetActive(false);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        bgmAudioSource.Pause();
        isPaused = true;
        SoundsManager.instance.PlaySound(buttonSound);
        blackBg.SetActive(true);
    }

    void OpenOptions()
    {
        settingMenu.OpenSettings();
        SoundsManager.instance.PlaySound(buttonSound);
    }

    public void GameOverScreen()
    {
        gameOverUI.SetActive(true);
        Pause();
    }

    void ExitGame()
    {
        SoundsManager.instance.PlaySound(buttonSound);
        SceneManager.LoadScene("Main menu");
    }

    void Retry()
    {
        Resume();
        health.Respawn();
        gameOverUI.SetActive(false);
    }

    void faceCheck()
    {
        if (switchClass.currentCharacterIndex == 0)
        {
            ariaFace.SetActive(true);
            ramaFace.SetActive(false);
        }
        else
        {
            ariaFace.SetActive(false);
            ramaFace.SetActive(true);
        }
    }
}
