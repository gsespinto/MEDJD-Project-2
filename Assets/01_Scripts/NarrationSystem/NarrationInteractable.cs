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

        narrationComponent = GameObject.FindObjectOfType<NarrationComponent>();
    }

    protected override void SetGazedAt(bool gazedAt)
    {
        if (!narrationComponent)
        {
            base.SetGazedAt(false);
            return;
        }

        if (ClipsHaveBeenQueued())
        {
            base.SetGazedAt(false);
            return;
        }

        base.SetGazedAt(gazedAt);
    }

    bool ClipsHaveBeenQueued()
    {
        foreach (FNarration narration in narrations)
        {
            if (!narrationComponent.HasClip(narration))
                return false;
        }

        return true;
    }

    public override bool CanInteract()
    {
        if (!narrationComponent)
        {
            return false;
        }

        if (ClipsHaveBeenQueued())
        {
            return false;
        }

        return base.CanInteract();
    }

    public override void OnInteraction()
    {
        // Null ref protection
        if (!narrationComponent)
        {
            Debug.LogWarning("Missing narration component reference.", this);
            return;
        }

        // Null ref protection
        if (narrations.Length <= 0)
        {
            Debug.LogWarning("Narration array empty.", this);
            return;
        }


        // Only interact if the interaction is loaded
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