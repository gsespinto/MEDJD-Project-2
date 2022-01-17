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
        if (polenVFX)
            polenVFX.SetActive(true);
    }

    protected override bool ReceiveItem()
    {
        if (hasReceivedPolen)
            return false;

        if (!base.ReceiveItem())
            return false;

        ChangeColor(afterColor, materialToChangeIndex);
        hasReceivedPolen = true;

        if (hasGivenPolen)
            Destroy(this);
        return true;
    }

    protected override void SetGazedAt(bool gazedAt)
    {
        if (containerScript.Item != EItem.NONE && hasReceivedPolen)
            base.SetGazedAt(false);

        if (containerScript.Item == EItem.NONE && hasGivenPolen)
            base.SetGazedAt(false);

        base.SetGazedAt(gazedAt);
    }

    protected override bool GiveItem()
    {
        if (hasGivenPolen)
            return false;

        if (!base.GiveItem())
            return false;

        hasGivenPolen = true;

        if (polenVFX)
            polenVFX.SetActive(false);

        if (hasReceivedPolen)
            Destroy(this);
        return true;
    }

    void ChangeColor(Color color, int materialIndex)
    {
        if (!flowerMesh)
            return;

        materialIndex = Mathf.Clamp(materialIndex, 0, flowerMesh.materials.Length - 1);
        Material newMat = flowerMesh.materials[materialIndex];
        newMat.color = color;
        flowerMesh.materials[materialIndex] = newMat;
    }

    public override bool CanInteract()
    {
        // Null ref protection
        if (!containerScript)
        {
            Debug.LogWarning("Missing container script reference.", this);
            return false;
        }

        bool canReceive = (acceptedItems.Contains(containerScript.Item) && containerScript.Giver != this) && !hasReceivedPolen;
        bool canGive = containerScript.Item == EItem.NONE && itemToGive != EItem.NONE && !hasGivenPolen;
        bool itemCondition = (itemToGive == EItem.NONE && acceptedItems.Count <= 0) || canGive || canReceive;

        // Has the interaction loaded?
        // Does the player have an item?
        return currentInteractionLoadTime <= 0 && itemCondition;
    }

    void GetColors()
    {
        FlowerColors flowerColors = GameObject.FindObjectOfType<FlowerColors>();
        if (!flowerColors)
            return;

        beforeColor = flowerColors.GetBeforeColor(itemToGive);
        afterColor = flowerColors.GetAfterColor(itemToGive);
    }
}

