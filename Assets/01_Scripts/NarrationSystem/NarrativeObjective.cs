﻿using UnityEngine;
using UnityEngine.EventSystems;

public class NarrativeObjective : NarrationInteractable
{
    [SerializeField] private int objectiveIndex; // Parent objective index
    [SerializeField] private string newDescription = "Finish listening."; // New description of the objective
    private bool hasQueuedClips; // Have the narration clips been queued
    private ObjectiveComponent objectiveComponent;

    protected override void Start()
    {
        base.Start();
        objectiveComponent = GameObject.FindObjectOfType<ObjectiveComponent>();
    }

    protected override void SetGazedAt(bool gazedAt)
    {
        // If it has already queued the clips
        // Can't be gazed at
        if (hasQueuedClips)
        {
            base.SetGazedAt(false);
            return;
        }

        base.SetGazedAt(gazedAt);
    }

    public override bool CanInteract()
    {
        // If it has already queued the clips
        // Can't interact
        if (hasQueuedClips)
            return false;

        return base.CanInteract();
    }

    public override void OnInteraction()
    {
        if (!CanInteract())
            return;

        base.OnInteraction();

        // Update parent objective description
        GameObject.FindObjectOfType<ObjectiveComponent>().UpdateObjective(objectiveIndex);
        // Has queued clips
        hasQueuedClips = true;
}

    /// <summary> If the narration component has finished playing the conversation clips, complete the objective </summary>
    void CompleteObjective()
    {
        // If hasn't queued clips
        // Do nothing
        if (!hasQueuedClips)
            return;

        // Null ref protection
        if (!narrationComponent)
            return;

        // If the clips haven't been queued
        // Or the narration component hasn't finished playing
        // Do nothing
        if (!hasQueuedClips || !narrationComponent.HasFinishedPlaying())
            return;

        // Null ref protection
        if (!objectiveComponent)
        {
            Debug.LogWarning("Couldn't find a valid reference to objective component.");
            return;
        }

        // Complete the parent objective index
        objectiveComponent.CompleteObjective(objectiveIndex);
    }

    protected override void Update()
    {
        base.Update();

        CompleteObjective();
    }
}