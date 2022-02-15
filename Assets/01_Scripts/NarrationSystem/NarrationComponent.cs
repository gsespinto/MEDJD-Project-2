using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarrationComponent : MonoBehaviour
{
#region Variables
    [SerializeField] private AudioSource[] narrationSources;
    private AudioSource currentNarrationSource;

    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private TextMeshProUGUI  captionText;
    private Color captionColor = Color.white;

    private List<FNarration> playedClips = new List<FNarration>();
    [SerializeField] private List<FNarration> clipsQueue = new List<FNarration>();
#endregion

    private void Start()
    {
        // Set caption as hidden
        // Get its color
        if (captionText)
        {
            captionText.gameObject.SetActive(false);
            captionColor = captionText.color;
        }
    }

    /// <summary> Checks if the given clip as been played, if not queues it to play </summary>
    public void PlayNarrationOnce(FNarration narration)
    {
        // Null ref protection
        if (narrationSources.Length <= 0)
            return;
        
        // If this clip as been played, do nothing
        if(playedClips.Contains(narration))
            return;

        PlayNarration(narration);

        // Add clip to played clips
        playedClips.Add(narration);
    }

    /// <summary> Queues given clip to play it to play </summary>
    public void PlayNarration(FNarration narration)
    {
        // Null ref protection
        if (narrationSources.Length <= 0)
            return;

        // Queue clip to play
        clipsQueue.Add(narration);
    }

    private void Update()
    {
        CheckForPause();
        PlayQueuedClips();
    }

    void CheckForPause()
    {
        if (Time.timeScale != 0)
        {
            if (AudioListener.pause)
                AudioListener.pause = false;
            return;
        }

        if (AudioListener.pause)
            return;

        AudioListener.pause = true;
            
    }

    /// <summary> Plays queued audio clips </summary>
    private void PlayQueuedClips()
    {
        // Null ref protection
        if (narrationSources.Length <= 0)
            return;

        // If there's no clips to be played
        if (clipsQueue.Count <= 0)
        {
            // If no narration is playing and caption is active
            // Reset values
            if (currentNarrationSource && !currentNarrationSource.isPlaying
            && captionText && captionText.gameObject.activeInHierarchy)
            {
                captionText.gameObject.SetActive(false);
                currentNarrationSource = null;
            }

            return;
        }

        // If the audio source is playing, do nothing
        if (currentNarrationSource != null && currentNarrationSource.isPlaying)
            return;

        // Set narration source that'll play
        currentNarrationSource = narrationSources[Mathf.Clamp(clipsQueue[0].sourceIndex, 0, narrationSources.Length - 1)];

        // Null ref protection
        if (!currentNarrationSource)
        {
            Debug.LogWarning("Invalid narration audio source reference.", this);
            return;
        }

        // Play clip
        currentNarrationSource.PlayOneShot(clipsQueue[0].clip);

        ShowCaption();

        // Remove clip from queue
        clipsQueue.RemoveAt(0);
    }

    /// <summary> Show current narration's caption </summary>
    void ShowCaption()
    {
        // Null ref protection
        if (!captionText)
        {
            Debug.LogWarning("Missing caption text mesh reference.", this);
            return;
        }

        // If the narration has an owner
        // Set its text properties
        string ownerText = "";
        if (clipsQueue[0].owner != "")
        {
            ownerText = "<color=#" + ColorUtility.ToHtmlStringRGB(clipsQueue[0].ownerColor) + "><b>" + clipsQueue[0].owner + ":</b> ";
        }

        // Caption text to show owner and caption with correspondent colors
        captionText.text = ownerText + "<color=#" + ColorUtility.ToHtmlStringRGB(captionColor) + ">" + clipsQueue[0].caption;
        // Show caption
        captionText.gameObject.SetActive(true);
    }

    /// <summary> Plays given clip as player SFX </summary>
    /// <param name="clip"></param>
    public void PlaySFX(AudioClip clip)
    {
        // Null ref protection
        if (!sfxSource)
        {
            Debug.LogWarning("Missing sfx source reference.", this);
            return;
        }

        sfxSource.PlayOneShot(clip);
    }

    /// <summary> Returns whether there's no clip to be played or being played </summary>
    public bool HasFinishedPlaying()
    {
        return clipsQueue.Count <= 0 && !currentNarrationSource;
    }

    /// <summary> Returns whether the given clip is in the queue or has been played already </summary>
    public bool HasClip (FNarration narration)
    {
        return clipsQueue.Contains(narration) || playedClips.Contains(narration);
    }
}
