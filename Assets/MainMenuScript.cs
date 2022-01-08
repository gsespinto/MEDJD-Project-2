using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] int firstGameSceneIndex;
    public void StartGame()
    {
        GameObject.FindObjectOfType<SceneLoader>().LoadScene(firstGameSceneIndex);
    }
}
