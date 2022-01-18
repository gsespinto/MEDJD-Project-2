using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerInteractable : ItemInteractable
{
    [SerializeField] GameObject polenVFX;
    [SerializeField] int materialToChangeIndex;
    [SerializeField] MeshRenderer flowerMesh;

    private bool hasGivenPolen = false;
    private bool hasReceivedPolen = false;

    [SerializeField] private Color beforeColor = Color.black;
    [SerializeField] private Color afterColor = Color.black;

    protected override void Start()
    {
        base.Start();

        GetColors();
        ChangeColor(beforeColor, materialToChangeIndex);
    }

    protected override void SetGazedAt(bool gazedAt)
    {
        // Null ref protection
        if (!containerScript)
        {
            Debug.LogWarning("Missing container script reference.", this);
            SetGazedAt(false);
            return;
        }

        // Can't receive polen if it already has
        if (containerScript.Item != EItem.NONE && hasReceivedPolen)
        {
            base.SetGazedAt(false);
            return;
        }

        // Can't give polen if it already has
        if (containerScript.Item == EItem.NONE && hasGivenPolen)
        {
            base.SetGazedAt(false);
            return;
        }

        base.SetGazedAt(gazedAt);
    }

    public override bool CanInteract()
    {
        // Null ref protection
        if (!containerScript)
        {
            Debug.LogWarning("Missing container script reference.", this);
            return false;
        }

        // Can receive item if the script accepts the item the player contains
        // If this isn't the item interactable that gave the item
        // And if it hasn't received polen
        bool canReceive = (acceptedItems.Contains(containerScript.Item) && containerScript.Giver != this) && !hasReceivedPolen;

        // Can give item if the player doens't hold any item
        // If this script has an item to give
        // And if it hasn't given polen
        bool canGive = containerScript.Item == EItem.NONE && itemToGive != EItem.NONE && !hasGivenPolen;

        bool itemCondition = canGive || canReceive;

        // Has the interaction loaded?
        // Does the player have an item?
        return currentInteractionLoadTime <= 0 && itemCondition;
    }

    protected override bool ReceiveItem()
    {
        // If it has received polen
        // Do nothing
        if (hasReceivedPolen)
            return false;

        // If can't receive the item
        // Do nothing
        if (!base.ReceiveItem())
            return false;

        // Receive polen and make visual changes
        hasReceivedPolen = true;
        ChangeColor(afterColor, materialToChangeIndex);

        DestroyScript();
        SetGazedAt(true);
        return true;
    }

    protected override bool GiveItem()
    {
        // If it has given polen
        // Do nothing
        if (hasGivenPolen)
            return false;

        // If can't give the item
        // Do nothing
        if (!base.GiveItem())
            return false;

        // Give polen and make visual changes
        hasGivenPolen = true;
        if (polenVFX)
            polenVFX.SetActive(false);

        DestroyScript();
        SetGazedAt(true);
        return true;
    }

    void ChangeColor(Color color, int materialIndex)
    {
        // Null ref protection
        if (!flowerMesh)
        {
            Debug.LogWarning("Missing flower mesh reference.", this);
            return;
        }

        // Clamp material index
        materialIndex = Mathf.Clamp(materialIndex, 0, flowerMesh.materials.Length - 1);
        // Set new material with given color
        Material newMat = flowerMesh.materials[materialIndex];
        newMat.color = color;
        // Assign material to flower mesh
        flowerMesh.materials[materialIndex] = newMat;
    }

    /// <summary> Gets before and after colors from FlowerColors script </summary>
    void GetColors()
    {
        // Get FlowerColors ref
        FlowerColors flowerColors = GameObject.FindObjectOfType<FlowerColors>();
        // Null ref protection
        if (!flowerColors)
        {
            Debug.LogWarning("Couldn't find valid reference to FlowerColors script in scene.", this);
            return;
        }

        // Get corrspondent before and after colors
        beforeColor = flowerColors.GetBeforeColor(itemToGive);
        afterColor = flowerColors.GetAfterColor(itemToGive);
    }

    /// <summary> If this flower has given and received polen, destroy script </summary>
    void DestroyScript()
    {
        if (!hasGivenPolen || !hasReceivedPolen)
            return;

        Destroy(this);
    }
}

