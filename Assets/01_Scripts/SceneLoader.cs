using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int sceneToLoad;
    private AsyncOperation loadOperation;
    [SerializeField] private Animator loadingAnimator;

    void Awake()
    {
        if (GameObject.FindObjectsOfType<SceneLoader>().Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);
    }

    public void LoadScene(int sceneIndex)
    {
        sceneToLoad = sceneIndex;
        loadingAnimator.SetTrigger("Load");
    }

    public void StartLoading()
    {
        loadOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        loadOperation.completed += UnloadScene;
    }

    void UnloadScene(AsyncOperation obj)
    {
        loadingAnimator.SetTrigger("Unload");
        loadOperation = null;
    }   
}
