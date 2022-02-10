using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSFX : InteractableComponent
{
    [Space(10)]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] sfxs;

    protected override void Effect()
    {
        PlayRandomSFX(sfxs);
    }

    void PlayRandomSFX(AudioClip[] sfxs)
    {
        if (sfxs.Length <= 0)
        {
            return;
        }

        if (!audioSource)
        {
            Debug.LogWarning("Missing audio source reference.", this);
            return;
        }

        int index = Random.Range(0, sfxs.Length - 1);
        audioSource.PlayOneShot(sfxs[index]);
    }
}
