using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarrationComponent : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI  text;
    private List<FNarration> playedClips = new List<FNarration>();
    private List<FNarration> clipsQueue = new List<FNarration>();
    
    private void Start()
    {
        if (text)
            text.gameObject.SetActive(false);
    }

    /// <summary> Checks if the given clip as been played, if not queues it to play </summary>
    public void PlayNarration(FNarration narration)
    {
        // Null ref protection
        if (!audioSource)
            return;
        
        // If this clip as been played, do nothing
        if(playedClips.Contains(narration))
            return;
        
        // Queue clip to play
        // Add clip to played clips
        clipsQueue.Add(narration);
        playedClips.Add(narration);
    }

    private void Update()
    {
        PlayQueuedClips();
    }

    /// <summary> Plays queued audio clips </summary>
    private void PlayQueuedClips()
    {
        // Null ref protection
        if (!audioSource)
            return;
        
        if (!audioSource.isPlaying && text.gameObject.activeInHierarchy)
        {
            text.gameObject.SetActive(false);
            return;
        }

        // If there's no clips to be played, do nothing
        if (clipsQueue.Count <= 0)
            return;

        // If the audio source is playing, do nothing
        if (audioSource.isPlaying)
            return;
        
        // Play first queued clip
        // And remove it from the queue
        audioSource.PlayOneShot(clipsQueue[0].clip);

        if (text)
        {
            text.text = clipsQueue[0].caption;
            text.gameObject.SetActive(true);
        }

        clipsQueue.RemoveAt(0);
    }
}

[System.Serializable]
public struct  FNarration
{
    public AudioClip clip;
    public string caption;
}
