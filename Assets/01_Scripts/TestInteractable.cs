using System;
using System.Collections;
using System.Collections.Generic;
using GoogleVR.HelloVR;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestInteractable : Interactable
{
    [Header("Interaction")] 
    [SerializeField] private GameObject objToDestroy;
    public override void OnInteraction(BaseEventData eventData)
    {
        if (!CanInteract())
            return;

        base.OnInteraction(eventData);
        if (!IsInteractInput(eventData))
            return;
        if (objToDestroy)
            Destroy(objToDestroy);
        Destroy(this);
    }
}
