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
        if (!vfxOrigin)
        {
            // Debug.LogWarning("Missing audio source reference.", this);
            return;
        }

        if (!vfxPrefab)
        {
            // Debug.LogWarning("Missing audio source reference.", this);
            return;
        }

        GameObject.Instantiate(vfxPrefab, vfxOrigin.transform.position, vfxOrigin.transform.rotation, this.transform.parent);
    }
}

