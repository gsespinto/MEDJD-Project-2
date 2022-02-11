using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAnimation : InteractableComponent
{
    [Space(10)]
    [SerializeField] private Animator animator;
    [SerializeField] private string trigger;

    protected override void Effect()
    {
        if (!animator)
        {
            Debug.LogWarning("Missing animator reference.", this);
            return;
        }

        animator.SetTrigger(trigger);
    }
}
