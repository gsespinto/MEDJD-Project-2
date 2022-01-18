using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveComponent : MonoBehaviour
{
    [SerializeField] private ObjectiveInfo[] objectives;
    [SerializeField] private int nextScene;
    [SerializeField] private TextMeshProUGUI objectivesText;
    bool finishedLevel = false;

    void Start()
    {
        SetObjectivesVisuals();
    }

    void SetObjectivesVisuals()
    {
        // Null ref protection
        if (!objectivesText)
        {
            Debug.LogWarning("Missing objectives text reference.", this);
            return;
        }

        // Reset objective text
        objectivesText.text = "";

        // Add each objective descript to a text mesh
        // With correct visual info
        foreach (ObjectiveInfo obj in objectives)
        {
            objectivesText.text += "<color=#" + ColorUtility.ToHtmlStringRGB(obj.ObjectiveColor) + ">" + obj.Description + " " + obj.AddedInformation +  "\n";
        }
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
        objectives[objectiveIndex].IsCompleted = true;
        // Update visuals
        SetObjectivesVisuals();

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
        GameObject.FindObjectOfType<SceneLoader>().LoadScene(nextScene);
    }

    /// <summary> Updates given objective with given description and updates visuals </summary>
    public void UpdateObjectiveDescription(int objectiveIndex, string newDescription)
    {
        // If there are no objectives
        // Do nothing
        if (objectives.Length <= 0)
            return;

        // Clamp given index
        objectiveIndex = Mathf.Clamp(objectiveIndex, 0, objectives.Length - 1);
        // Change objective description
        objectives[objectiveIndex].Description = newDescription;

        // Update visuals
        SetObjectivesVisuals();
    }

    /// <summary> Updates given objective with given additional information and updates visuals </summary>
    public void UpdateObjectiveAddedInformation(int objectiveIndex, string addedInformation)
    {
        if (objectives.Length <= 0)
            return;

        // Clamp given index
        objectiveIndex = Mathf.Clamp(objectiveIndex, 0, objectives.Length - 1);
        // Change additional information
        objectives[objectiveIndex].AddedInformation = addedInformation;

        // Update visuals
        SetObjectivesVisuals();
    }
}

[System.Serializable]

public class ObjectiveInfo
{
    [SerializeField] private string description;
    [SerializeField] private string additionalInformation;
    private bool isCompleted;
    [SerializeField] private Color color;

    /// <summary> Is the objective completed </summary>
    public bool IsCompleted
    {
        get { return isCompleted; }

        set 
        {
            isCompleted = value;

            // If the objective has been completed
            // Strike out the objective's description and additional information
            if (isCompleted)
            {
                description = "<s>" + description;
                additionalInformation = additionalInformation + "</s>";
            }
        }
    }

    /// <summary> Objective's description </summary>
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    /// <summary> Additional information to the objective's description </summary>
    public string AddedInformation
    {
        get { return additionalInformation; }
        set { additionalInformation = value; }
    }

    /// <summary> Objective's text color </summary>
    public Color ObjectiveColor
    {
        get { return color; }
        set { color = value; }
    }
}
