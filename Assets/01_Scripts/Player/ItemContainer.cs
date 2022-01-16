using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    private EItem item = EItem.NONE;
    [SerializeField] protected Image itemIcon;

    public EItem Item 
    {
        get { return item; }
        
        set {
            item = value;

            if(itemIcon)
                itemIcon.gameObject.SetActive(item != EItem.NONE);
        } 
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
