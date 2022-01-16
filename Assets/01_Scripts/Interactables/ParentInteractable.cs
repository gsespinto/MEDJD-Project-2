using UnityEngine;
using UnityEngine.EventSystems;

public class ParentInteractable : NarrationInteractable
{
    [SerializeField] private int objectiveIndex; // Parent objective index
    [SerializeField] private string newDescription = "Finish listening to parent."; // New description of the objective
    private bool hasQueuedClips; // Have the narration clips been queued

    public override void OnInteraction(BaseEventData eventData)
    {
        if (!CanInteract())
            return;

        base.OnInteraction(eventData);

        // Update parent objective description
        GameObject.FindObjectOfType<ObjectiveComponent>().UpdateObjectiveDescription(objectiveIndex, newDescription);
        // Has queued clips
        hasQueuedClips = true;
    }

    /// <summary> If the narration component has finished playing the conversation clips, complete the objective </summary>
    void CompleteObjective()
    {
        // Null ref protection
        if (!narrationComponent)
            return;

        // If the clips haven't been queued
        // Or the narration component hasn't finished playing
        // Do nothing
        if (!hasQueuedClips || !narrationComponent.HasFinishedPlaying())
            return;

        // Complete the parent objective index
        Objective.CompleteObjective(objectiveIndex);
    }

    protected override void Update()
    {
        base.Update();

        CompleteObjective();
    }
}
