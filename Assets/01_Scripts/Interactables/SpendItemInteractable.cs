using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpendItemInteractable : Interactable
{
    [Header("Interaction")]
    [SerializeField] private List<EItem> acceptedItems = new List<EItem>();
    private ItemContainer containerScript;
    [SerializeField] private int objectiveIndex;
    ScoreScript scoreScript;

    protected override void Start()
    {
        base.Start();

        containerScript = GameObject.FindObjectOfType<ItemContainer>();

        ScoreScript[] scoreScripts = GameObject.FindObjectsOfType<ScoreScript>();

        foreach (ScoreScript sc in scoreScripts)
        {
            if (sc.objectiveIndex == objectiveIndex)
            {
                scoreScript = sc;
                break;
            }
        }
    }

    protected override void SetGazedAt(bool gazedAt)
    {
        // Null ref protection
        if (!containerScript)
        {
            Debug.LogWarning("Missing container script reference.", this);
            base.SetGazedAt(false);
            return;
        }

        // Can't spend an item you don't have
        if (containerScript.Item == EItem.NONE)
        {
            base.SetGazedAt(false);
            return;
        }

        base.SetGazedAt(gazedAt);
    }

    public override bool CanInteract()
    {
        // Null ref protection
        if (!containerScript)
        {
            Debug.LogWarning("Missing container script reference.", this);
            return false;
        }

        // Has the interaction loaded?
        // Does the player have the item?
        return currentInteractionLoadTime <= 0 && acceptedItems.Contains(containerScript.Item);
    }

    public override void OnInteraction(BaseEventData eventData)
    {
        if (!CanInteract())
            return;

        scoreScript.ChangeScore(+1);
        containerScript.Item = EItem.NONE;

        Destroy(this);

        base.OnInteraction(eventData);
    }
}
