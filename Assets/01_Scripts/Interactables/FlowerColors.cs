using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerColors : MonoBehaviour
{
    [SerializeField] FPolenColor[] flowerColors;

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
