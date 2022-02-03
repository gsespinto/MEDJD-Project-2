using UnityEngine;
using UnityEngine.EventSystems;

public class DEPRECATED_TreeInteractable : Interactable
{
    [Header("Interaction")] 
    [SerializeField] private float destroyTimer = 0;
    [SerializeField] private Object[] objsToDestroy;
    [SerializeField] private AudioClip[] shoutClips;

    public override void OnInteraction()
    {
        if (!CanInteract())
            return;

        ScoreScript scoreScript = GameObject.FindObjectOfType<ScoreScript>();
        // Null ref protection
        if (!scoreScript)
        {
            Debug.LogWarning("Missing score script reference.", this);
            return;
        }
        
        // Increase score
        scoreScript.ChangeScore(+1);
        //Play SFX
        PlayShoutClip();
        // Destroy objects
        DestroyObjects();

        base.OnInteraction();
    }

    /// <summary> Plays random sfx from shout clips </summary>
    void PlayShoutClip()
    {
        // If there are no shout clips
        // Do nothing
        if (shoutClips.Length <= 0)
            return;
       
        NarrationComponent narrationComponent = GameObject.FindObjectOfType<NarrationComponent>();
        // Null ref protection
        if (!narrationComponent)
        {
            Debug.LogWarning("Missing narration component reference.", this);
            return;
        }

        // Play shout SFX
        narrationComponent.PlaySFX(shoutClips[Random.Range(0, shoutClips.Length)]);
    }

    /// <summary> Destroy the objects of objsToDestroy with set timer and then itself </summary>
    void DestroyObjects()
    {
        foreach (Object obj in objsToDestroy)
        {
            Destroy(obj, destroyTimer);
        }

        Destroy(this);
    }
}
