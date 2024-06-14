using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject settingsMenuUI;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Button backBtn;

    [Header("Audio")]
    [SerializeField] AudioClip buttonSound;
    [SerializeField] GameObject bgmObject;
    [SerializeField] GameObject sfxObject;
    AudioSource bgmAudioSource;
    AudioSource sfxAudioSource;

    void Start()
    {
        bgmAudioSource = bgmObject.GetComponent<AudioSource>();
        sfxAudioSource = sfxObject.GetComponent<AudioSource>();

        bgmSlider.value = bgmAudioSource.volume;
        sfxSlider.value = sfxAudioSource.volume;

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        backBtn.onClick.AddListener(CloseSettings);

        settingsMenuUI.SetActive(false);
    }

    void SetBGMVolume(float volume)
    {
        bgmAudioSource.volume = volume;
    }

    void SetSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;
    }

    public void OpenSettings()
    {
        settingsMenuUI.SetActive(true);
    }

    void CloseSettings()
    {
        SoundsManager.instance.PlaySound(buttonSound);
        settingsMenuUI.SetActive(false);
    }
}
