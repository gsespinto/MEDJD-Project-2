using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDestroyObjects : InteractableComponent
{
    [Space(10)]
    [SerializeField, Min(0.1f)] float destroyTimer;
    [SerializeField] private Object[] objectsToDestroy;

    protected override void Effect()
    {
        foreach (Object obj in objectsToDestroy)
        {
            if (obj == interactable)
            {
                Destroy(obj, destroyTimer + 0.1f);
                continue;
            }

            Destroy(obj, destroyTimer);
        }
    }
}
