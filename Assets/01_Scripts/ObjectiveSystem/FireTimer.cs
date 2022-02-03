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
        if (hasEnded)
            return;

        currentFireTimer -= Time.deltaTime;

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

    void UpdateAudioVisuals()
    {
        if (fireSlider)
            fireSlider.value = currentFireTimer / fireTimer;

        foreach (AudioSource audio in fireAudioSources)
            audio.volume = 1 - currentFireTimer / fireTimer;

        foreach (ParticleSystem ps in smokes)
        {
            ParticleSystem.MainModule mainModule = ps.main;
            mainModule.startSize = smokeParticleTargetSize * (1 - currentFireTimer / fireTimer);
        }
    }
}
