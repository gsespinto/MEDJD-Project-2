using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public static void CompleteObjective(int index)
    {
        ObjectiveComponent objectiveComponent = GameObject.FindObjectOfType<ObjectiveComponent>();

        if (!objectiveComponent)
        {
            return;
        }

        objectiveComponent.CompleteObjective(index);
    }
}
