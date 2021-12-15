using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : InteractableWithVisuals
{
    [Header("Visuals")] 
    [SerializeField] private GameObject objectToDestroy;
    
    /// <summary> Destroys target object and this script upon interaction </summary>
    public  override void Interaction()
    {
        base.LoadInteraction();
        
        if (currentInteractionLoadTime > 0)
            return;

        Destroy(objectToDestroy);
        Destroy(canvas.gameObject);
        Destroy(this);
    }
}
