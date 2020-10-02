using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private int sceneIndex;

    [Inject]
    private ZenjectSceneLoader loader;

    void Start()
    {
        if (!SceneIsLoaded(sceneIndex))
        {
            loader.LoadSceneAsync(sceneIndex, loadMode: UnityEngine.SceneManagement.LoadSceneMode.Additive/*, containerMode: LoadSceneRelationship.Child*/);
        }
        else
        {
            Debug.Log("Scene already loaded");
        }
    }

    public bool SceneIsLoaded(int sceneIndex)
    {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            if (SceneManager.GetSceneAt(i).buildIndex == sceneIndex)
                return true;
        }
        return false;
    }


}