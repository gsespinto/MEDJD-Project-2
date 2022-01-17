using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInteractable : Interactable
{

    [Header("Interaction")]
    [SerializeField] private int objectiveIndex;
    protected ItemContainer containerScript;
    ScoreScript scoreScript;

    [Header("Giving")]
    [SerializeField] protected EItem itemToGive;

    [Header("Receiving")]
    [SerializeField] protected List<EItem> acceptedItems = new List<EItem>();

    [Header("Destroying")]
    [SerializeField] private float destroyTimer = 0; // Seconds after which to destroy the objects in the list
    [SerializeField] private List<Object> objectsToDestroy = new List<Object>(); // List of objects to destroy upon interaction

    protected override void Start()
    {
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

        base.Start();
    }

    protected override void SetGazedAt(bool gazedAt)
    {
        // Null ref protection
        // can't gaze
        if (!containerScript)
        {
            Debug.LogWarning("Missing container script reference.", this);
            base.SetGazedAt(false);
            return;
        }

        if (containerScript.Giver == this)
        {
            return;
        }

        // If the player has no item
        // and there's no item to give
        // can't gaze
        if (containerScript.Item == EItem.NONE && itemToGive == EItem.NONE)
        {
           base.SetGazedAt(false);
            return;
        }
        
        // If the player has an item
        // and this script doesn't accept it
        // can't gaze
        if (containerScript.Item != EItem.NONE && !acceptedItems.Contains(containerScript.Item))
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

        bool canReceive = (acceptedItems.Contains(containerScript.Item) && containerScript.Giver != this);
        bool canGive = containerScript.Item == EItem.NONE && itemToGive != EItem.NONE;
        bool itemCondition = (itemToGive == EItem.NONE && acceptedItems.Count <= 0) || canGive || canReceive;

        // Has the interaction loaded?
        // Does the player have an item?
        return currentInteractionLoadTime <= 0 && itemCondition;
    }

    public override void OnInteraction(BaseEventData eventData)
    {
        if (!CanInteract())
            return;

        if (containerScript.Item != EItem.NONE)
        {
            ReceiveItem();
        }
        else
            GiveItem();

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

    /// <summary> If there's an item to give, gives it to the item container </summary>
    protected virtual bool GiveItem()
    {
        if (!containerScript)
            return false;

        if (itemToGive == EItem.NONE)
            return false;

        // Add item to player
        containerScript.SetItem(itemToGive, this);
        return true;
    }

    /// <summary> If the item container has an accepted item, take it </summary>
    protected virtual bool ReceiveItem()
    {
        if (!containerScript)
            return false;

        if (acceptedItems.Count <= 0)
            return false;

        if (!scoreScript)
        {
            Debug.LogWarning("Missing score script reference.", this);
            return false;
        }

        scoreScript.ChangeScore(+1);
        containerScript.SetItem(EItem.NONE, null);
        return true;
    }
}
