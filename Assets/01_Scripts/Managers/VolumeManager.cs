using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] float minVolume = -60.0f;
    [Space(10)]
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource narrationSource;
    [SerializeField] AudioSource ambientSource;
    [Space(10)]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider narrationSlider;
    [SerializeField] private Slider ambientSlider;

    void Awake()
    {
        // Update audio sliders values to match mixer
        ChangeAudioSliderValue(sfxSlider, "SFX");
        ChangeAudioSliderValue(musicSlider, "Music");
        ChangeAudioSliderValue(narrationSlider, "Narration");
        ChangeAudioSliderValue(ambientSlider, "Ambient");
        
        // Pause audio sources
        PlaySFXAudio(false);
        PlayNarrationAudio(false);
        PlayAmbientAudio(false);
    }

    /// <summary> Sets given slider value to match given group of audio mixer </summary>
    void ChangeAudioSliderValue(Slider slider, string groupName)
    {
        // Null ref protection
        if(!audioMixer)
        {
            Debug.LogWarning("Missing audio mixer reference.", this);
            return;
        }

        // Null ref protection
        if (!slider)
        {
            Debug.LogWarning("Invalid slider reference.", this);
            return;
        }

        float value;

        if (!audioMixer.GetFloat(groupName, out value))
        {
            // Null ref protection
            Debug.LogWarning("Couldn't find " + groupName + " group in Audio mixer.", this);
            return;
        }
        
        // Set given slider value
        slider.value = value;
    }

    /// <summary> Change given audio mixer group volume </summary>
    void ChangeAudioGroupVolume(string groupName, float value)
    {
        // Null ref protection
        if (!audioMixer)
        {
            Debug.LogWarning("Missing audio mixer reference.", this);
            return;
        }

        // If the value is at mininum
        // Mute audio group
        if (value <= minVolume)
        {
            audioMixer.SetFloat(groupName, -100);
            return;
        }

        // Set group's volume to given value
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

    /// <summary> Plays and pauses given audio source </summary>
    void PlayAudioSource(AudioSource audioSource, bool play)
    {
        // Null ref protection
        if (!audioSource)
        {
            Debug.LogWarning("Invalid audio source reference.", this);
            return;
        }

        if (play)
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
