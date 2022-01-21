using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] int firstGameSceneIndex;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] float minVolume = -60.0f;
    [Space(20)]
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource narrationSource;
    [SerializeField] AudioSource ambientSource;
    [Space(20)]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider narrationSlider;
    [SerializeField] private Slider ambientSlider;

    void Awake()
    {
        PlaySFXAudio(false);
        PlayMusicAudio(false);
        PlayNarrationAudio(false);
        PlayAmbientAudio(false);

        ChangeAudioSliderValue(sfxSlider, "SFX");
        ChangeAudioSliderValue(musicSlider, "Music");
        ChangeAudioSliderValue(narrationSlider, "Narration");
        ChangeAudioSliderValue(ambientSlider, "Ambient");
    }

    public void StartGame()
    {
        GameObject.FindObjectOfType<SceneLoader>().LoadScene(firstGameSceneIndex);
    }

    void ChangeAudioSliderValue(Slider slider, string groupName)
    {
        float value;
        audioMixer.GetFloat(groupName, out value);
        slider.value = value;
    }


    void ChangeAudioGroupVolume(string groupName, float value)
    {
        if (value <= minVolume)
        {
            audioMixer.SetFloat(groupName, -100);
            return;
        }

        audioMixer.SetFloat(groupName, value);
    }

    public void ChangeMasterVolume(System.Single value)
    {
        ChangeAudioGroupVolume("Master", value);
    }

    public void ChangeSFXVolume(System.Single value)
    {
        ChangeAudioGroupVolume("SFX", value);
    }

    public void ChangeMusicVolume(System.Single value)
    {
        ChangeAudioGroupVolume("Music", value);
    }

    public void ChangeNarrationVolume(System.Single value)
    {
        ChangeAudioGroupVolume("Narration", value);
    }
    public void ChangeAmbientVolume(System.Single value)
    {
        ChangeAudioGroupVolume("Ambient", value);
    }

    void PlayAudioSource(AudioSource audioSource, bool play)
    {
        if(play)
        {
            audioSource.Play();
            return;
        }

        audioSource.Pause();
    }

    public void PlaySFXAudio(bool play)
    {
        PlayAudioSource(sfxSource, play);
    }
    public void PlayMusicAudio(bool play)
    {
        PlayAudioSource(musicSource, play);
    }
    public void PlayNarrationAudio(bool play)
    {
        PlayAudioSource(narrationSource, play);
    }
    public void PlayAmbientAudio(bool play)
    {
        PlayAudioSource(ambientSource, play);
    }
}
