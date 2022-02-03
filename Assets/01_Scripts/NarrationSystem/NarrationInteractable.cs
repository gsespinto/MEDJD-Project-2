using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NarrationInteractable : Interactable
{
    [SerializeField] private FNarration[] narrations;
    protected NarrationComponent narrationComponent;

    protected override void Start()
    {
        base.Start();

        // Get narration component ref
        narrationComponent = GameObject.FindObjectOfType<NarrationComponent>();
    }

    protected override void SetGazedAt(bool gazedAt)
    {
        // If it has already queued the narrations
        // Can't be gazed at
        if (ClipsHaveBeenQueued())
        {
            base.SetGazedAt(false);
            return;
        }

        base.SetGazedAt(gazedAt);
    }

    /// <returns> True if all narrations have been queued in the narration component </returns>
    bool ClipsHaveBeenQueued()
    {
        // Null ref protection
        if (narrations.Length <= 0)
        {
            Debug.LogWarning("Narration array empty.", this);
            return true;
        }

        // Null ref protection
        if (!narrationComponent)
        {
            Debug.LogWarning("Invalid narration component reference.", this);
            return true;
        }

        foreach (FNarration narration in narrations)
        {
            if (!narrationComponent.HasClip(narration))
                return false;
        }

        return true;
    }

    public override bool CanInteract()
    {
        // If it has already queued the narrations
        // Can't be interacted with
        if (ClipsHaveBeenQueued())
        {
            return false;
        }

        return base.CanInteract();
    }

    public override void OnInteraction()
    {
        if (!CanInteract())
            return;

        // Add the narrations to the queue
        foreach (FNarration n in narrations)
        {
            narrationComponent.PlayNarrationOnce(n);
        }

        base.OnInteraction();
    }
}