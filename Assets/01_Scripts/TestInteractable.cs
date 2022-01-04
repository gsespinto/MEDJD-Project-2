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
    [SerializeField] private GameObject[] objsToDestroy;
    
    public override void OnInteraction(BaseEventData eventData)
    {
        if (!CanInteract())
            return;

        base.OnInteraction(eventData);
        if (!IsInteractInput(eventData))
            return;

        GameObject.FindObjectOfType<ScoreScript>().ChangeScore(+1);
        
        foreach (GameObject obj in objsToDestroy)
        {
            Destroy(obj);
        }
        
        Destroy(this);
    }
}
