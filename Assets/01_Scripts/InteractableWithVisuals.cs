using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableWithVisuals : Interactable
{
    [Header("Visuals")] 
    [SerializeField] private Image progressBar;
    [SerializeField] protected Canvas canvas;
    [SerializeField] protected string tag = "";
    private GameObject player;
    
    // Update is called once per frame
    void Start()
    {
        StopLoadingInteraction();
        canvas.worldCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag(tag);
    }

    private void Update()
    {
        ShowProgress();
    }

    /// <summary> Shows the progress of the loading of the interaction </summary>
    private void ShowProgress()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag(tag);
            return;
        }
        
        progressBar.fillAmount = currentInteractionLoadTime / interactionLoadTime;
    }

    /// <summary> Show progress canvas </summary>
    public override void LoadInteraction()
    {
        base.LoadInteraction();
        if (!player)
            return;
        
        canvas.enabled = true;
        if (player)
            canvas.transform.LookAt(player.transform);
    }

    /// <summary> Hide progress canvas </summary>
    public override void StopLoadingInteraction()
    {
        canvas.enabled = false;
    }
}
