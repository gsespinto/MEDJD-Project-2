using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInteractable : Interactable
{

    [Header("Interaction")]
    protected ItemContainer containerScript;

    [Header("Giving")]
    [SerializeField] protected EItem itemToGive;

    [Header("Receiving")]
    [SerializeField] protected List<EItem> acceptedItems = new List<EItem>();

    protected override void Start()
    {
        // Get ContainerScript ref
        containerScript = GameObject.FindObjectOfType<ItemContainer>();
        AssignDelegates();

        base.Start();
    }

    /// <summary> Assigns functions to item container events </summary>
    protected virtual void AssignDelegates()
    {
        // Null ref protection
        if (!containerScript)
        {
            Debug.LogWarning("Missing container script reference.", this);
            return;
        }

        // Whenever the player picks up an item
        // Reset interaction load time
        containerScript.OnSetItem += RechargeInteraction;
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

        // If this is the current giving script
        // Can't be gazed
        if (containerScript.Giver == this)
        {
            base.SetGazedAt(false);
            return;
        }

        // If the player has no item
        // and there's no item to give
        // Can't be gaze
        if (containerScript.CurrentItem == EItem.NONE && itemToGive == EItem.NONE)
        {
            base.SetGazedAt(false);
            return;
        }
        
        // If the player has an item
        // and this script doesn't accept it
        // can't gaze
        if (containerScript.CurrentItem != EItem.NONE && !acceptedItems.Contains(containerScript.CurrentItem))
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

        // Can receive item if the script accepts the item the player contains
        // If this isn't the item interactable that gave the item
        bool canReceive = (acceptedItems.Contains(containerScript.CurrentItem) && containerScript.Giver != this);

        // Can give item if the player doens't hold any item
        // If this script has an item to give
        bool canGive = containerScript.CurrentItem == EItem.NONE && itemToGive != EItem.NONE;
        
        bool itemCondition = canGive || canReceive;

        // Has the interaction loaded?
        // Does the player have an item?
        return base.CanInteract() && itemCondition;
    }

    public override void Interact()
    {
        if (!CanInteract())
            return;

        base.Interact();

        // If the player holds an item
        // Then try to receive it
        if (containerScript.CurrentItem != EItem.NONE)
        {
            ReceiveItem();
        }
        // If the player has no item
        // Then try to give it
        else
            GiveItem();
    }

    /// <summary> If there's an item to give, gives it to the item container </summary>
    /// <returns> If it has successfully given the item </returns>
    protected virtual bool GiveItem()
    {
        // Null ref protection
        if (!containerScript)
        {
            Debug.LogWarning("Missing container script reference.", this);
            return false;
        }

        // If it hasn't any item to give
        // Do nothing
        if (itemToGive == EItem.NONE)
            return false;

        // Add item to player
        containerScript.SetItem(itemToGive, this);

        return true;
    }

    /// <summary> If the item container has an accepted item, take it </summary>
    /// <returns> If it has successfully received an item </returns>
    protected virtual bool ReceiveItem()
    {
        // Null ref protection
        if (!containerScript)
        {
            Debug.LogWarning("Missing container script reference.", this);
            return false;
        }

        // If this script doesn't accept any items
        // Do nothing
        if (acceptedItems.Count <= 0)
            return false;

        // Reset containter script item value
        containerScript.SetItem(EItem.NONE, null);

        return true;
    }
}
