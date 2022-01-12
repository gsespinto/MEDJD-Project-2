using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarrationComponent : MonoBehaviour
{
    [SerializeField] private AudioSource narrationSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private TextMeshProUGUI  captionText;
    private List<FNarration> playedClips = new List<FNarration>();
    [SerializeField] private List<FNarration> clipsQueue = new List<FNarration>();
    
    private void Start()
    {
        if (captionText)
            captionText.gameObject.SetActive(false);
    }

    /// <summary> Checks if the given clip as been played, if not queues it to play </summary>
    public void PlayNarration(FNarration narration)
    {
        // Null ref protection
        if (!narrationSource)
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
        if (!narrationSource)
            return;
        
        // If no narration isn't playing and caption is active
        // Hide caption
        if (!narrationSource.isPlaying
        && captionText 
        && captionText.gameObject.activeInHierarchy)
        {
            captionText.gameObject.SetActive(false);
            return;
        }

        // If there's no clips to be played, do nothing
        if (clipsQueue.Count <= 0)
            return;

        // If the audio source is playing, do nothing
        if (narrationSource.isPlaying)
            return;
        
        // Play first queued clip
        // And remove it from the queue
        narrationSource.PlayOneShot(clipsQueue[0].clip);

        // If the caption text ref is valid
        // Set text to clip's caption and activate object
        if (captionText)
        {
            captionText.text = clipsQueue[0].caption;
            captionText.gameObject.SetActive(true);
        }

        // Remove clip from queue
        clipsQueue.RemoveAt(0);
    }

    public void PlaySFX(AudioClip clip)
    {
        // Null ref protection
        if (!sfxSource)
            return;

        sfxSource.PlayOneShot(clip);
    }
}

[System.Serializable]
public struct  FNarration
{
    public AudioClip clip;
    public string caption;
}
