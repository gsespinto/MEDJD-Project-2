using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickUpInteractable : Interactable
{
    [Header("Effect")]
    [SerializeField] private EItem itemToGive;
    [SerializeField] private float destroyTimer = 0; // Seconds after which to destroy the objects in the list
    [SerializeField] private List<Object> objectsToDestroy = new List<Object>(); // List of objects to destroy upon interaction
    private ItemContainer containerScript;

    protected override void Start()
    {
        base.Start();

        containerScript = GameObject.FindObjectOfType<ItemContainer>();
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
        
        // Can't pickup an item you already have
        if (containerScript.Item != EItem.NONE)
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
        // Does the player have an item?
        return currentInteractionLoadTime <= 0 &&
                containerScript.Item == EItem.NONE;
    }

    public override void OnInteraction(BaseEventData eventData)
    {
        if (!CanInteract())
            return;

        // Add item to player
        containerScript.Item = itemToGive;

        base.OnInteraction(eventData);

        DestroyObjects();
    }

    /// <summary> Destroy each object of list to destroy after set time </summary>
    protected virtual void DestroyObjects()
    {
        if (objectsToDestroy.Count <= 0)
            return;

        Destroy(objectsToDestroy[0], destroyTimer);
        objectsToDestroy.RemoveAt(0);
    }
}
