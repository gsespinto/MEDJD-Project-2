using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class MusicScript : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    void Start()
    {
        foreach (MusicScript ms in GameObject.FindObjectsOfType<MusicScript>())
        {
            if (ms == this)
                continue;

            ms.FadeOut();
        }

        animator = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();

        DontDestroyOnLoad(this.gameObject);
    }

    public void FadeOut()
    {
        if (!animator)
        {
            Debug.Log("Missing animator reference.", this);
            Destroy(this);
            return;
        }

        if (!audioSource)
        {
            Debug.Log("Missing audio source reference.", this);
            Destroy(this);
            return;
        }

        animator.SetTrigger("FadeOut");
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
