using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireTimer : MonoBehaviour
{
    [SerializeField, Min(0)] private float fireTimer;
    private float currentFireTimer;
    [SerializeField] private Slider fireSlider;
    [SerializeField] private AudioSource[] fireAudioSources;
    [SerializeField] private ParticleSystem[] smokes;
    [SerializeField] private float smokeParticleTargetSize = 0.5f;
    [SerializeField] private int badEndingLevelIndex = 0;
    bool hasEnded;

    // Start is called before the first frame update
    void Start()
    {
        currentFireTimer = fireTimer;
    }

    // Update is called once per frame
    void Update()
    {
        TickTimer();
    }

    void TickTimer()
    {
        // If timer has already ended
        // Do nothing
        if (hasEnded)
            return;

        // Tick timer
        currentFireTimer -= Time.deltaTime;

        // If the timer has run out
        // End level and load bad ending
        if (currentFireTimer <= 0)
        {
            hasEnded = true;
            LoadBadEnding();
        }

        UpdateAudioVisuals();
    }

    void LoadBadEnding()
    {
        LoadingManager.LoadScene(badEndingLevelIndex);
    }

    /// <summary> Updates fire slider, audio sources and particle systems </summary>
    void UpdateAudioVisuals()
    {
        // Update fire timer
        if (fireSlider)
            fireSlider.value = currentFireTimer / fireTimer;

        // Update volume of each fire audio source
        foreach (AudioSource audio in fireAudioSources)
            audio.volume = 1 - currentFireTimer / fireTimer;

        // Update size of each fire particle system
        foreach (ParticleSystem ps in smokes)
        {
            ParticleSystem.MainModule mainModule = ps.main;
            mainModule.startSize = smokeParticleTargetSize * (1 - currentFireTimer / fireTimer);
        }
    }
}
