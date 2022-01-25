using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomAnimationController : MonoBehaviour
{
    bool bIsTalking = false;
    [SerializeField] AudioSource momAudioSource;
    [SerializeField] Animator momAnimator;
    
    void TalkIdle()
    {
        if (!momAudioSource)
        {
            Debug.LogWarning("Missing mom audio source reference.", this);
            return;
        }

        if (!momAnimator)
        {
            Debug.LogWarning("Missing mom animator reference.", this);
            return;
        }

        if (!bIsTalking && momAudioSource.isPlaying)
        {
            momAnimator.SetTrigger("Talk");
            bIsTalking = true;
            return;
        }

        if (bIsTalking && !momAudioSource.isPlaying)
        {
            momAnimator.SetTrigger("Idle");
            bIsTalking = false;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        TalkIdle();
    }
}
