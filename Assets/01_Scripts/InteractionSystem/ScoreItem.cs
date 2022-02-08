using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : ItemInteractable
{
    [Header("Interaction")]
    [SerializeField] private int objectiveIndex;
    ScoreScript scoreScript;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Get correspondent ScoreScript ref from scene
        ScoreScript[] scoreScripts = GameObject.FindObjectsOfType<ScoreScript>();
        foreach (ScoreScript sc in scoreScripts)
        {
            if (sc.objectiveIndex == objectiveIndex)
            {
                scoreScript = sc;
                break;
            }
        }
    }



    protected override bool ReceiveItem()
    {
        // Null ref protection
        if (!scoreScript)
        {
            Debug.LogWarning("Missing score script reference.", this);
            return false;
        }

        // If has received item
        if (base.ReceiveItem())
        {
            // Increase score
            scoreScript.ChangeScore(+1);
            return true;
        }

        return false;
    }

    protected override bool GiveItem()
    {
        // Null ref protection
        if (!scoreScript)
        {
            Debug.LogWarning("Missing score script reference.", this);
            return false;
        }

        // If has received item
        if (base.GiveItem())
        {
            // Update objective
            scoreScript.UpdateObjective(objectiveIndex);
            return true;
        }

        return false;
    }
}
