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
    public void PlayNarration(FNarration narration)
    {
        // Null ref protection
        if (narrationSources.Length <= 0)
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
        if (narrationSources.Length <= 0)
            return;

        // If no narration is playing and caption is active
        // Hide caption
        // Reset currentNarrationSource ref
        if (currentNarrationSource && !currentNarrationSource.isPlaying
        && captionText && captionText.gameObject.activeInHierarchy)
        {
            captionText.gameObject.SetActive(false);
            currentNarrationSource = null;
            return;
        }

        // If there's no clips to be played, do nothing
        if (clipsQueue.Count <= 0)
            return;

        // If the audio source is playing, do nothing
        if (currentNarrationSource != null && currentNarrationSource.isPlaying)
            return;

        // Set narration source that'll play
        currentNarrationSource = narrationSources[Mathf.Clamp(clipsQueue[0].sourceIndex, 0, narrationSources.Length - 1)];

        // Play clip
        currentNarrationSource.PlayOneShot(clipsQueue[0].clip);

        // If the caption text ref is valid
        // Set text to clip's caption and activate object
        if (captionText)
        {
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

        // Remove clip from queue
        clipsQueue.RemoveAt(0);
    }

    /// <summary> Plays given clip as player SFX </summary>
    /// <param name="clip"></param>
    public void PlaySFX(AudioClip clip)
    {
        // Null ref protection
        if (!sfxSource)
            return;

        sfxSource.PlayOneShot(clip);
    }
}

[System.Serializable]
public struct FNarration
{
    /// <summary> Audio clip to be played </summary>
    public AudioClip clip;
    /// <summary> Caption of audio clip </summary>
    public string caption;
    /// <summary> The index of the audio source of the narration component that will play this narration </summary>
    public int sourceIndex;
    /// <summary> Name of the owner of this narration </summary>
    public string owner;
    /// <summary> Color to display the name of the owner </summary>
    public Color ownerColor;
}
