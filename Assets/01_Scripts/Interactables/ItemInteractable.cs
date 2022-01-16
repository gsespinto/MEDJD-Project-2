using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInteractable : Interactable
{
    ScoreScript scoreScript;
    private ItemContainer containerScript;

    [Header("Interaction")]
    [SerializeField] private int objectiveIndex;

    [Header("Giving")]
    [SerializeField] private EItem itemToGive;

    [Header("Receiving")]
    [SerializeField] private List<EItem> acceptedItems = new List<EItem>();

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
        if (!containerScript)
        {
            Debug.LogWarning("Missing container script reference.", this);
            base.SetGazedAt(false);
            return;
        }

        if (containerScript.Item == EItem.NONE && itemToGive == EItem.NONE)
        {
           base.SetGazedAt(false);
            return;
        }
        
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

        bool givingCondition = itemToGive == EItem.NONE || containerScript.Item == EItem.NONE || acceptedItems.Contains(containerScript.Item);
        bool receivingCondition = acceptedItems.Count <= 0 || acceptedItems.Contains(containerScript.Item) || containerScript.Item == EItem.NONE;

        // Has the interaction loaded?
        // Does the player have an item?
        return currentInteractionLoadTime <= 0 && givingCondition && receivingCondition;
    }

    public override void OnInteraction(BaseEventData eventData)
    {
        if (!CanInteract())
            return;

        if (containerScript.Item != EItem.NONE)
        {
            if (acceptedItems.Count <= 0)
                return;

            if (!scoreScript)
            {
                Debug.LogWarning("Missing score script reference.", this);
                return;
            }
            
            scoreScript.ChangeScore(+1);
            containerScript.Item = EItem.NONE;

            base.OnInteraction(eventData);
            return;
        }

        if (itemToGive == EItem.NONE)
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
