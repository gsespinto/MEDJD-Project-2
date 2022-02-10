using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerContainer : ItemContainer
{
    private FlowerColors flowerColors;

    protected override void Start()
    {
        // Get FlowerColors component
        flowerColors = this.GetComponent<FlowerColors>();

        base.Start();
    }

    /// <summary> Sets current item being held by container and the object that gave it </summary>
    public override void SetItem(EItem _item, ItemInteractable _giver)
    {
        base.SetItem(_item, _giver);

        // Null ref protection
        if (!flowerColors)
        {
            Debug.LogWarning("Missing flower colors script reference.", this);
            return;
        }

        // Change item icon color to match given item
        itemImage.color = flowerColors.GetAfterColor(_item);
    }
}
