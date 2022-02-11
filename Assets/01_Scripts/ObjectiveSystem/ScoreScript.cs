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
        UpdateVisuals();
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

        UpdateVisuals();
        RefreshObjective(objectiveIndex);

        // If score has reached its target value
        // Complete objective
        if (currentScore >= targetScore)
            CompleteObjective();
    }

    /// <summary> Update objective's additional information to show current score </summary>
    void UpdateVisuals()
    {
        string info = "[" + currentScore + " / " + targetScore + "]";
        objectiveComponent.UpdateObjectiveInfo(objectiveIndex, info);
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

    public void RefreshObjective(int index)
    {
        // Null ref protection
        if (!objectiveComponent)
        {
            Debug.LogWarning("Missing objectives component reference.", this);
            return;
        }

        objectiveComponent.OnRefreshObjective?.Invoke(index);
    }
}
