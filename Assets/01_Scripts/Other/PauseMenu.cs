using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Interactable
{

    public static bool GameIsPaused = false;

    [SerializeField] private GameObject[] pausedObjects;
    [SerializeField] private GameObject[] unpausedObjects;


    protected override void Start()
    {
        Resume();
    }

    public override void Interact()
    {
        TogglePause();
        base.Interact();
    }

    void TogglePause()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public override bool CanInteract()
    {
        return currentInteractionLoadTime <= 0;
    }

    void Resume()
    {
        foreach (GameObject obj in unpausedObjects)
            obj.SetActive(true);

        foreach (GameObject obj in pausedObjects)
            obj.SetActive(false);

        AudioListener.pause = false;
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    void Pause()
    {
        foreach (GameObject obj in unpausedObjects)
            obj.SetActive(false);

        foreach (GameObject obj in pausedObjects)
            obj.SetActive(true);

        AudioListener.pause = true;
        GameIsPaused = true;
        Time.timeScale = 0f;
    }
}
