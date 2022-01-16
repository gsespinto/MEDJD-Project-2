using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public int objectiveIndex;
    [SerializeField] int targetScore = 5;
    int currentScore = 0;

    void Start()
    {
        // Sets initial visual values
        ChangeScore(0);
    }

    /// <summary> Change score by given amount, if it has reached target score, load next scene </summary>
    public void ChangeScore(int amount)
    {
        ObjectiveComponent objectiveComponent = GameObject.FindObjectOfType<ObjectiveComponent>();

        if (!objectiveComponent)
            return;

        // Change score by given amount
        currentScore += amount;
        // Update visuals
        string info = "[" + currentScore + " / " + targetScore + "]";
        GameObject.FindObjectOfType<ObjectiveComponent>().UpdateObjectiveAddedInformation(objectiveIndex, info);

        // If score has reached its target value
        // Complete objective
        if (currentScore >= targetScore)
            Objective.CompleteObjective(objectiveIndex);
    }
}
