using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractableComponent : MonoBehaviour
{
    /// <summary> Interactable to add the effect to </summary>
    [SerializeField] protected Interactable interactable;
    /// <summary> Delegate to assign the effect to </summary>
    [SerializeField] private EInteractionDelegate delegateType;

    // Start is called before the first frame update
    void Awake()
    {
        AssignDelegate();
    }

    /// <summary> Assings component's effect to correspondent interactable delegate </summary>
    void AssignDelegate()
    {
        // Get interactable ref if not set yet
        if (!interactable)
        {
            interactable = this.GetComponent<Interactable>();

            // Null ref protection
            if (!interactable)
            {
                Debug.LogWarning("Couldn't find valid reference of Interactable script.", this);
                Destroy(this);
                return;
            }
        }

        // Add effect to correspondent delegate
        switch (delegateType)
        {
            case EInteractionDelegate.ON_INTERACTION:
                interactable.OnInteraction += Effect;
                break;
            case EInteractionDelegate.ON_GAZED_AT_FALSE:
                interactable.OnGazedAt += OnGazedAtFalse;
                break;
            case EInteractionDelegate.ON_GAZED_AT_TRUE:
                interactable.OnGazedAt += OnGazedAtTrue;
                break;

            default:
                break;
        }
    }

    /// <summary> Execute component's effect when pointer enters the interactable </summary>
    void OnGazedAtTrue(bool gazedAt)
    {
        if (!gazedAt)
            return;

        Effect();
    }

    /// <summary> Execute component's effect when pointer leaves the interactable </summary>
    void OnGazedAtFalse(bool gazedAt)
    {
        if (gazedAt)
            return;

        Effect();
    }

    /// <summary> Component's effect </summary>
    protected virtual void Effect()
    {
        return;
    }
}
