using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractableComponent : MonoBehaviour
{
    [SerializeField] protected Interactable interactable;
    [SerializeField] private EInteractionDelegate delegateType;

    // Start is called before the first frame update
    void Awake()
    {
        AssignDelegate();
    }

    void AssignDelegate()
    {
        if (!interactable)
        {
            interactable = this.GetComponent<Interactable>();
            if (!interactable)
            {
                Debug.LogWarning("Couldn't find valid reference of Interactable script.", this);
                return;
            }
        }

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

    void OnGazedAtTrue(bool gazedAt)
    {
        if (!gazedAt)
            return;

        Effect();
    }

    void OnGazedAtFalse(bool gazedAt)
    {
        if (gazedAt)
            return;

        Effect();
    }

    protected virtual void Effect()
    {
        return;
    }
}
