using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    private EItem currentItem = EItem.NONE;
    private ItemInteractable giver;

    [Header("Visuals")]
    [SerializeField] protected Image itemImage;
    [SerializeField] private FItemIcon[] itemIcons;

    private void Awake()
    {
        SetItem(EItem.NONE, null);
    }

    /// <summary> Item that is being held </summary>
    public EItem CurrentItem 
    {
        get { return currentItem; }
    }

    /// <summary> Script that has given the item </summary>
    public ItemInteractable Giver 
    {
        get { return giver; }
    }

    /// <summary> Sets the item being held and the script that gave it </summary>
    /// <param name="_item"> Item to be held </param>
    /// <param name="_giver"> Reference to script that is giving the item </param>
    public virtual void SetItem(EItem _item, ItemInteractable _giver)
    {
        // Update values to correspond to given
        currentItem = _item;
        giver = _giver;

        UpdateItemIcon();
    }

    /// <summary> Hides item icon if there's no item being held, else shows it </summary>
    void UpdateItemIcon()
    {
        // Null ref protection
        if (!itemImage)
        {
            return;
        }

        // Update sprite of image according to current item
        foreach(FItemIcon icon in itemIcons)
        {
            if (icon.item != currentItem)
                continue;

            itemImage.sprite = icon.icon;
        }

        // Only show icon image if player is holding an item
        itemImage.gameObject.SetActive(currentItem != EItem.NONE);
    }
}
