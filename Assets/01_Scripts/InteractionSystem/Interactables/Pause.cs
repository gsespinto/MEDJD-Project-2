using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : Interactable
{
    public override void Interact()
    {
            if (Time.timeScale == 0)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;
    }

    public override bool CanInteract()
    {
        return true;
    }
}
