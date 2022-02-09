using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObjetive : MonoBehaviour
{
    [SerializeField] private int objectiveIndex;
    [SerializeField] private TutorialVisuals[] tutorialVisuals;
    private int currentTutorialPhase = 0;
    ObjectiveComponent objectiveComponent;

    private void Start()
    {
        objectiveComponent = GameObject.FindObjectOfType<ObjectiveComponent>();
        SetInitialValues();
    }

    void SetInitialValues()
    {
        // Null ref protection
        if (!objectiveComponent)
        {
            Debug.LogWarning("Missing objective component reference.", this);
            return;
        }
        
        // Call UpdateTutorial whenever an objective is update
        objectiveComponent.OnRefreshObjective += UpdateTutorial;

        // Show the first step tutorial visuals and hide the rest
        for (int i = 0; i < tutorialVisuals.Length; i++)
        {
            if (i == 0)
            {
                foreach (GameObject obj in tutorialVisuals[i].objects)
                    obj.SetActive(true);

                continue;
            }

            foreach (GameObject obj in tutorialVisuals[i].objects)
                obj.SetActive(false);
        }
    }

    void UpdateTutorial(int index)
    {
        // If the updated objective is not the one relevant to this tutorial
        // Do nothing
        if (index != objectiveIndex)
            return;

        // Go to next step of tutorial
        currentTutorialPhase++;

        // If tutorial has ended
        // Destroy all tutorial visuals and this script
        if (currentTutorialPhase > tutorialVisuals.Length - 1)
        {
            foreach (TutorialVisuals tv in tutorialVisuals)
                foreach (GameObject obj in tv.objects)
                    Destroy(obj);

            Destroy(this);
            return;
        }

        // If the tutorial is still on going
        // Hide last step's visuals
        foreach (GameObject obj in tutorialVisuals[currentTutorialPhase - 1].objects)
            obj.SetActive(false);
        // Show this step's visuals
        foreach (GameObject obj in tutorialVisuals[currentTutorialPhase].objects)
            obj.SetActive(true);
    }
}

[System.Serializable]
public class TutorialVisuals
{
    public GameObject[] objects;
}
