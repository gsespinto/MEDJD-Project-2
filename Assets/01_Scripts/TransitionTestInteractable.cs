using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTestInteractable : InteractableWithVisuals
{
    [SerializeField] private GameObject oldController;
    [SerializeField] private GameObject newController;
    
    public override void Interaction()
    {
        base.Interaction();
        
        if (currentInteractionLoadTime > 0)
            return;
        
        newController.SetActive(true);
        oldController.SetActive(false);
        Destroy(canvas.gameObject);
        Destroy(this.gameObject);
    }
}
