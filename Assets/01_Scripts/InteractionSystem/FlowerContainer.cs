﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerContainer : ItemContainer
{
    private FlowerColors flowerColors;

    private void Start()
    {
        // Get FlowerColors component
        flowerColors = this.GetComponent<FlowerColors>();
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
        itemIcon.color = flowerColors.GetAfterColor(_item);
    }
}
