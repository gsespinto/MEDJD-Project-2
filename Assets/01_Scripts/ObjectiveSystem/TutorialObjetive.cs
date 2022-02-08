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

        if (objectiveComponent)
            objectiveComponent.OnUpdateObjective += UpdateTutorial;

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
        if (index != objectiveIndex)
            return;

        currentTutorialPhase++;
        if (currentTutorialPhase > tutorialVisuals.Length - 1)
        {
            foreach (TutorialVisuals tv in tutorialVisuals)
                foreach (GameObject obj in tv.objects)
                    Destroy(obj);

            Destroy(this);
            return;
        }

        foreach (GameObject obj in tutorialVisuals[currentTutorialPhase - 1].objects)
            obj.SetActive(false);

        foreach (GameObject obj in tutorialVisuals[currentTutorialPhase].objects)
            obj.SetActive(true);
    }
}

[System.Serializable]
public class TutorialVisuals
{
    public GameObject[] objects;
}
