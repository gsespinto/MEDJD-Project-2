using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableVFX : InteractableComponent
{
    [Space(10)]
    [SerializeField] private Transform vfxOrigin;
    [SerializeField] private GameObject vfxPrefab;

    protected override void Effect()
    {
        // Null ref protection
        if (!vfxOrigin)
        {
            Debug.LogWarning("Missing vfx origin reference.", this);
            return;
        }

        // Null ref protection
        if (!vfxPrefab)
        {
            Debug.LogWarning("Missing vfx prefab reference.", this);
            return;
        }

        // Instantiate vfx
        GameObject.Instantiate(vfxPrefab, vfxOrigin.transform.position, vfxOrigin.transform.rotation, this.transform.parent);
    }
}

