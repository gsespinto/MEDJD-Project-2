using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    /// <summary> Completes objective component's objective with given index </summary>
    public static void CompleteObjective(int index)
    {
        ObjectiveComponent objectiveComponent = GameObject.FindObjectOfType<ObjectiveComponent>();
        // Null ref protection
        if (!objectiveComponent)
        {
            Debug.LogWarning("Couldn't find a valid reference to objective component.");
            return;
        }

        // Complete given objective
        objectiveComponent.CompleteObjective(index);
    }
}
