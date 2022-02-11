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
        // Destroy each object in objects to destroy
        foreach (Object obj in objectsToDestroy)
        {
            // if the interactable is to be destroyed
            // add an extra time to be the last object being destroyed 
            // because of dependencies
            if (obj == interactable)
            {
                Destroy(obj, destroyTimer + 0.1f);
                continue;
            }

            Destroy(obj, destroyTimer);
        }
    }
}
