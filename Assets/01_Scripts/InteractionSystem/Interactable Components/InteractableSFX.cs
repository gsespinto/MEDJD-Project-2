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

    /// <summary> Plays random audio clip from given array </summary>
    void PlayRandomSFX(AudioClip[] sfxs)
    {
        // Null ref protection
        if (sfxs.Length <= 0)
        {
            return;
        }

        // Null ref protection
        if (!audioSource)
        {
            Debug.LogWarning("Missing audio source reference.", this);
            return;
        }

        // Play random sfx
        int index = Random.Range(0, sfxs.Length - 1);
        audioSource.PlayOneShot(sfxs[index]);
    }
}
