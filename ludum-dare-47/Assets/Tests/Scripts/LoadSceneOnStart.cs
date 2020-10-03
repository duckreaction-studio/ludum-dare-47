using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LoadSceneOnStart : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    [Inject]
    private ZenjectSceneLoader loader;

    void Start()
    {
        if (!SceneIsLoaded(sceneName))
        {
            loader.LoadSceneAsync(sceneName, loadMode: LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("Scene already loaded");
        }
    }

    public bool SceneIsLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
                return true;
        }
        return false;
    }


}