using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveComponent : MonoBehaviour
{
    [SerializeField] private ObjectiveInfo[] objectives;
    [SerializeField] private int nextScene;
    [SerializeField] private float objectiveRefreshTime = 1.0f;
    [SerializeField] private FNarration[] objectiveIndicators;
    bool finishedLevel = false;

    void Start()
    {
        foreach (ObjectiveInfo oi in objectives)
            oi.ResetReminder();
    }

    private void Update()
    {
        TickObjectivesReminder();
    }
    public void CompleteObjective(int objectiveIndex)
    {
        // If there aren't any objectives
        // Or the level as finished
        // Do nothing
        if (objectives.Length <= 0 || finishedLevel)
            return;

        // Clamp given index
        objectiveIndex = Mathf.Clamp(objectiveIndex, 0, objectives.Length - 1);
        // Complete objective
        objectives[objectiveIndex].CompleteObjective();

        if (HasCompletedAllObjectives())
            FinishLevel();
    }

    /// <summary> Returns whether all objectives have been completed </summary>
    bool HasCompletedAllObjectives()
    {
        foreach (ObjectiveInfo obj in objectives)
        {
            if (!obj.IsCompleted)
                return false;
        }

        return true;
    }

    /// <summary> Load new level </summary>
    void FinishLevel()
    {

        finishedLevel = true;
        LoadingFunctionLibrary.LoadScene(nextScene);
    }

    /// <summary> Updates given objective with given description and updates visuals </summary>
    public void UpdateObjective(int objectiveIndex)
    {
        // If there are no objectives
        // Do nothing
        if (objectives.Length <= 0)
            return;

        // Clamp given index
        objectiveIndex = Mathf.Clamp(objectiveIndex, 0, objectives.Length - 1);
        objectives[objectiveIndex].ResetReminder();
    }

    /// <summary> Updates given objective with given additional information and updates visuals </summary>
    public void UpdateObjective(int objectiveIndex, string addedInformation)
    {
        if (objectives.Length <= 0)
            return;

        // Clamp given index
        objectiveIndex = Mathf.Clamp(objectiveIndex, 0, objectives.Length - 1);
        // Change additional information
        objectives[objectiveIndex].SetAdditionalInformation(addedInformation);
        objectives[objectiveIndex].ResetReminder();
    }

    void TickObjectivesReminder()
    {
        foreach (ObjectiveInfo oi in objectives)
        {
            oi.TickReminder();
        }
    }
}