
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerInteractable : ScoreItem
{
    [Header("Flower")]
    [SerializeField] ParticleSystem polenVFX;
    [SerializeField] Image arrow;
    [SerializeField] int materialToChangeIndex;
    [SerializeField] MeshRenderer flowerMesh;

    private bool hasGivenPolen = false;
    private bool hasReceivedPolen = false;

    private Color beforeColor = Color.black;
    private Color afterColor = Color.black;

    protected override void Start()
    {
        base.Start();

        GetColors();
        ChangeColor(beforeColor, materialToChangeIndex);
    }

    protected override void AssignDelegates()
    {
        if (!containerScript)
        {
            Debug.LogWarning("Missing container script reference.", this);
            return;
        }

        containerScript.OnSetItem += HandleArrow;
        containerScript.OnSetItem += HandlePolenVFX;
        base.AssignDelegates();
    }

    /// <summary> Handles flower's arrow visibility </summary>
    void HandleArrow()
    {
        // Null ref protection
        if (!arrow || !containerScript)
            return;

        // If the play has an item and this script isn't the giver
        // Show arrow
        if (containerScript.CurrentItem != EItem.NONE && containerScript.Giver != this)
        {
            arrow.gameObject.SetActive(true);
            return;
        }

        // If the player hasn't got an item or this is the giver
        // Hide arrow
        arrow.gameObject.SetActive(false);
    }

    /// <summary> Handles flower's polen vfx visibility </summary>
    void HandlePolenVFX()
    {
        // Null ref protection
        if (!polenVFX || !containerScript)
            return;

        // If the player hasn't got an item
        // Play polen vfx
        if (containerScript.CurrentItem == EItem.NONE)
        {
            polenVFX.Play(); ;
            return;
        }

        // If the player has an item
        // Stop polen VFX
        polenVFX.Stop();
    }

    protected override void SetGazedAt(bool gazedAt)
    {
        // Null ref protection
        if (!containerScript)
        {
            Debug.LogWarning("Missing container script reference.", this);
            base.SetGazedAt(false);
            return;
        }

        // Can't receive polen if it already has
        if (containerScript.CurrentItem != EItem.NONE && hasReceivedPolen)
        {
            base.SetGazedAt(false);
            return;
        }

        // Can't give polen if it already has
        if (containerScript.CurrentItem == EItem.NONE && hasGivenPolen)
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
        bool canReceive = (acceptedItems.Contains(containerScript.CurrentItem) && containerScript.Giver != this) && !hasReceivedPolen;

        // Can give item if the player doens't hold any item
        // If this script has an item to give
        // And if it hasn't given polen
        bool canGive = containerScript.CurrentItem == EItem.NONE && itemToGive != EItem.NONE && !hasGivenPolen;

        bool itemCondition = canGive || canReceive;

        // Has the interaction loaded?
        // Does the player have an item?
        return base.CanInteract() && itemCondition;
    }

    /// <summary> Attempts to receive item from container script </summary>
    /// <returns> If has received item successfully </returns>
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
        if (arrow)
            Destroy(arrow);
        ChangeColor(afterColor, materialToChangeIndex);

        DestroyObjects();
        return true;
    }

    /// <summary> Attempts to give item from container script </summary>
    /// <returns> If has given item successfully </returns>
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
            Destroy(polenVFX);

        DestroyObjects();
        return true;
    }

    public override void Interact()
    {
        base.Interact();
        SetGazedAt(true);
    }

    /// <summary> Changes mesh's material color of the given index </summary>
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
        if (arrow)
            arrow.color = afterColor;
    }

    /// <summary> If this flower has given and received polen, destroy objects </summary>
    protected override void DestroyObjects()
    {
        if (!hasGivenPolen || !hasReceivedPolen)
            return;

        base.DestroyObjects();
    }
}

