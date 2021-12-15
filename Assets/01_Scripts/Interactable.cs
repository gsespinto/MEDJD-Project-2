using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected float interactionLoadTime = 1.0f; // Time to load interaction function
    protected float currentInteractionLoadTime;

    // Start is called before the first frame update
    void Awake()
    {
        currentInteractionLoadTime = interactionLoadTime;
    }

    ///<summary> Tick the interaction timer </summary> 
    public virtual void LoadInteraction()
    {
        if (currentInteractionLoadTime <= 0)
            return;
        
        currentInteractionLoadTime -= Time.deltaTime;
    }

    ///<summary> Called when the loading of the interaction is cancelled </summary>
    public virtual void StopLoadingInteraction()
    {
        return;
    }
    
    ///<summary> Effect of the interaction </summary>
    public virtual void Interaction()
    {
        if (currentInteractionLoadTime > 0)
            return;
    }
}
