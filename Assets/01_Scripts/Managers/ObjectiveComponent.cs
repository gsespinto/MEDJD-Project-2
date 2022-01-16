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
        if (!objectivesText)
            return;

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
        if (objectives.Length <= 0 || finishedLevel)
            return;

        objectiveIndex = Mathf.Clamp(objectiveIndex, 0, objectives.Length - 1);
        objectives[objectiveIndex].IsCompleted = true;
        SetObjectivesVisuals();

        foreach (ObjectiveInfo obj in objectives)
        {
            if (!obj.IsCompleted)
                return;
        }

        finishedLevel = true;
        GameObject.FindObjectOfType<SceneLoader>().LoadScene(nextScene);
    }

    public void UpdateObjectiveDescription(int objectiveIndex, string newDescription)
    {
        if (objectives.Length <= 0)
            return;

        objectiveIndex = Mathf.Clamp(objectiveIndex, 0, objectives.Length - 1);
        objectives[objectiveIndex].Description = newDescription;

        SetObjectivesVisuals();
    }

    public void UpdateObjectiveAddedInformation(int objectiveIndex, string addedInformation)
    {
        if (objectives.Length <= 0)
            return;

        objectiveIndex = Mathf.Clamp(objectiveIndex, 0, objectives.Length - 1);
        objectives[objectiveIndex].AddedInformation = addedInformation;

        SetObjectivesVisuals();
    }
}

[System.Serializable]

public class ObjectiveInfo
{
    [SerializeField] private string description;
    [SerializeField] private string addedInformation;
    private bool isCompleted;
    [SerializeField] private Color color;

    public bool IsCompleted
    {
        get { return isCompleted; }

        set 
        {
            isCompleted = value;
            description = "<s>" + description;
            addedInformation = addedInformation + "</s>";
        }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public string AddedInformation
    {
        get { return addedInformation; }
        set { addedInformation = value; }
    }

    public Color ObjectiveColor
    {
        get { return color; }
        set { color = value; }
    }
}
