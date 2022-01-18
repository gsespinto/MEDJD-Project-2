using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerColors : MonoBehaviour
{
    [SerializeField] FPolenColor[] flowerColors;

    /// <summary> Returns before color of given item type, if no found returns black </summary>
    public Color GetBeforeColor(EItem type)
    {
        foreach (FPolenColor pc in flowerColors)
        {
            if (pc.flower == type)
            {
                return pc.beforeColor;
            }
        }

        return Color.black;
    }

    /// <summary> Returns after color of given item type, if no found returns black </summary>
    public Color GetAfterColor(EItem type)
    {
        foreach (FPolenColor pc in flowerColors)
        {
            if (pc.flower == type)
            {
                return pc.afterColor;
            }
        }

        return Color.black;
    }
}

[System.Serializable]
public struct FPolenColor
{
    public EItem flower;
    public Color beforeColor;
    public Color afterColor;
}
