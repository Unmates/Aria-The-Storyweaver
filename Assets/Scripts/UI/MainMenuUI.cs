using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI")]
    SettingMenu settingMenu;
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject blackBg;
    public Button start_btn, setting_btn, exit_btn;
    [SerializeField] AudioClip buttonSound;

    private void OnEnable()
    {
        start_btn.onClick.AddListener(StartGame);
        setting_btn.onClick.AddListener(Settings);
        exit_btn.onClick.AddListener(ExitProgram);
    }

    private void OnDisable()
    {
        start_btn.onClick.RemoveAllListeners();
    }

    void Start()
    {
        settingMenu = GetComponent<SettingMenu>();
        blackBg.SetActive(false);
    }

    private void Update()
    {
        BlackBGActive();
    }

    void StartGame()
    {
        SoundsManager.instance.PlaySound(buttonSound);
        SceneManager.LoadScene("Level_1");
    }

    void Settings()
    {
        settingMenu.OpenSettings();
        blackBg.SetActive(true);
        SoundsManager.instance.PlaySound(buttonSound);
        Debug.Log("Setting button pressed");
    }

    void ExitProgram()
    {
        SoundsManager.instance.PlaySound(buttonSound);
        // Exit to main menu or close the application
        Debug.Log("Exit button clicked");

        #if UNITY_EDITOR
        // If running in the Unity Editor, log a message
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If running in a build, quit the application
        Application.Quit();
        #endif
    }

    void BlackBGActive()
    {
        if (settingUI.activeSelf == true)
        {
            blackBg.SetActive(true);
        }
        else
        {
            blackBg.SetActive(false);
        }
    }
}
