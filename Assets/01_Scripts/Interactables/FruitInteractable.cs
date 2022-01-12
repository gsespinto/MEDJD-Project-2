using UnityEngine;
using UnityEngine.EventSystems;

public class FruitInteractable : Interactable
{
    [Header("Interaction")]
    private ChildScript childScript;

    protected override void Start()
    {
        base.Start();

        childScript = GameObject.FindObjectOfType<ChildScript>();
    }

    protected override void SetGazedAt(bool gazedAt)
    {
        if (!childScript || childScript.HasFruit)
        {
            base.SetGazedAt(false);
            return;
        }

        base.SetGazedAt(gazedAt);
    }

    public override bool CanInteract()
    {
        if (!childScript)
            return false;

        return currentInteractionLoadTime <= 0 && !childScript.HasFruit;
    }

    public override void OnInteraction(BaseEventData eventData)
    {
        if (!CanInteract())
            return;

        childScript.HasFruit = true;

        base.OnInteraction(eventData);
    }
}
