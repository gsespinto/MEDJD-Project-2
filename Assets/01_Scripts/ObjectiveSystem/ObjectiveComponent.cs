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

    public delegate void UpdateObjectiveCallback(int objetiveIndex);
    public UpdateObjectiveCallback OnUpdateObjective;

    void Awake()
    {
        OnUpdateObjective += UpdateObjective;
    }

    void Start()
    {
        foreach (ObjectiveInfo oi in objectives)
            oi.StartFunction();
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
        LoadingManager.LoadScene(nextScene);
    }

    /// <summary> Updates given objective with given additional information and updates visuals </summary>
    private void UpdateObjective(int objectiveIndex)
    {
        if (objectives.Length <= 0)
            return;

        // Clamp given index
        objectiveIndex = Mathf.Clamp(objectiveIndex, 0, objectives.Length - 1);
        // Change additional information
        objectives[objectiveIndex].ResetReminder();
    }

    public void UpdateObjectiveInfo(int objectiveIndex, string addedInformation)
    {
        if (objectives.Length <= 0)
            return;

        // Clamp given index
        objectiveIndex = Mathf.Clamp(objectiveIndex, 0, objectives.Length - 1);
        objectives[objectiveIndex].SetAdditionalInformation(addedInformation);
    }

    void TickObjectivesReminder()
    {
        foreach (ObjectiveInfo oi in objectives)
        {
            oi.TickReminder();
        }
    }
}