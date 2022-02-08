using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public int objectiveIndex;
    [SerializeField] int targetScore = 5;
    int currentScore = 0;
    ObjectiveComponent objectiveComponent;

    void Start()
    {
        objectiveComponent = this.GetComponent<ObjectiveComponent>();
        // Sets initial visual values
        ChangeScore(0);
    }

    /// <summary> Change score by given amount, if it has reached target score, load next scene </summary>
    public void ChangeScore(int amount)
    {
        // Null ref protection
        if (!objectiveComponent)
        {
            Debug.LogWarning("Missing objectives component reference.", this);
            return;
        }

        // Change score by given amount
        currentScore += amount;
        // Update visuals
        string info = "[" + currentScore + " / " + targetScore + "]";
        objectiveComponent.UpdateObjectiveInfo(objectiveIndex, info);
        objectiveComponent.OnUpdateObjective(objectiveIndex);

        // If score has reached its target value
        // Complete objective
        if (currentScore >= targetScore)
            CompleteObjective();
    }

    /// <summary> If the narration component has finished playing the conversation clips, complete the objective </summary>
    void CompleteObjective()
    {
        // Null ref protection
        if (!objectiveComponent)
        {
            Debug.LogWarning("Missing objectives component reference.", this);
            return;
        }

        // Complete the parent objective index
        objectiveComponent.CompleteObjective(objectiveIndex);
    }

    public void UpdateObjective(int index)
    {
        // Null ref protection
        if (!objectiveComponent)
        {
            Debug.LogWarning("Missing objectives component reference.", this);
            return;
        }

        objectiveComponent.OnUpdateObjective(index);
    }
}
