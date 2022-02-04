using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    /// <summary> Loads scene with given index </summary>
    public static void LoadScene(int sceneIndex)
    {
        SceneLoader sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
        if(!sceneLoader)
        {
            return;
        }

        sceneLoader.LoadScene(sceneIndex);
    }

    /// <summary> Loads scene with sceneToLoad index </summary>
    public void LoadScene()
    {
        LoadScene(sceneToLoad);
    }
}
