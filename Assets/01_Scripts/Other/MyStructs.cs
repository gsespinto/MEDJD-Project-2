using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct FNarration
{
    /// <summary> Audio clip to be played </summary>
    public AudioClip clip;
    /// <summary> Caption of audio clip </summary>
    public string caption;
    /// <summary> The index of the audio source of the narration component that will play this narration </summary>
    public int sourceIndex;
    /// <summary> Name of the owner of this narration </summary>
    public string owner;
    /// <summary> Color to display the name of the owner </summary>
    public Color ownerColor;
}

[System.Serializable]
public struct FPolenColor
{
    public EItem flower;
    public Color beforeColor;
    public Color afterColor;
}

[System.Serializable]
public struct FItemIcon
{
    public EItem item;
    public Sprite icon;
}
