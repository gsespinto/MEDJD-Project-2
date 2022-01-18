using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    private EItem item = EItem.NONE;
    [SerializeField] protected Image itemIcon;
    private ItemInteractable giver;

    private void Awake()
    {
        SetItem(EItem.NONE, null);
    }

    /// <summary> Item that is being held </summary>
    public EItem Item 
    {
        get { return item; }
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
        item = _item;
        giver = _giver;

        UpdateItemIconVisibility();
    }

    /// <summary> Hides item icon if there's no item being held, else shows it </summary>
    void UpdateItemIconVisibility()
    {
        // Null ref protection
        if (!itemIcon)
        {
            return;
        }

        itemIcon.gameObject.SetActive(item != EItem.NONE);
    }
}

[System.Serializable]
public enum EItem
{
    NONE,
    APPLE,
    RED_POLEN,
    BLUE_POLEN,
}
