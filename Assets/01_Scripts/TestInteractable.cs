using System;
using System.Collections;
using System.Collections.Generic;
using GoogleVR.HelloVR;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestInteractable : Interactable
{
    [SerializeField] protected float interactionLoadTime = 1.0f; // Time to load interaction function
    protected float currentInteractionLoadTime;
    private bool loadingInteraction = false;

    void Start()
    {
        currentInteractionLoadTime = interactionLoadTime;
    }

    private void Update()
    {
        LoadInteraction();
    }

    public override void SetGazedAt(bool gazedAt)
    {
        loadingInteraction = gazedAt;
    }

    private void LoadInteraction()
    {
        if (!loadingInteraction)
            return;

        currentInteractionLoadTime -= Time.deltaTime;
    }

    public override void Interaction(BaseEventData eventData)
    {
        base.Interaction(eventData);
        
        if (!IsInteractInput(eventData))
            return;
        
        Destroy(this.gameObject);
    }
}
