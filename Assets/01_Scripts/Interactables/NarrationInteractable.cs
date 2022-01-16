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

    public override void OnInteraction(BaseEventData eventData)
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
            narrationComponent.PlayNarration(n);
        }

        base.OnInteraction(eventData);
    }
}