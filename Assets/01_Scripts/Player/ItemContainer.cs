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

    public EItem Item 
    {
        get { return item; }
        
        set { item = value; } 
    }

    public ItemInteractable Giver 
    {
        get { return giver; }
        
        set { giver = value; } 
    }


    public void SetItem(EItem _item, ItemInteractable _giver)
    {
        Item = _item;
        Giver = _giver;

        if (itemIcon)
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
