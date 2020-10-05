using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    [Inject]
    private ZenjectSceneLoader _loader;

    [Inject]
    private SceneTransitionEffect _sceneTransition;

    public void LoadScene()
    {
        _sceneTransition.StartFadeInEffect(OnScreenIsBlack);
    }

    private void OnScreenIsBlack()
    {
        _loader.LoadSceneAsync(sceneName, loadMode: UnityEngine.SceneManagement.LoadSceneMode.Single).completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperation obj)
    {
        _sceneTransition.StartFadeOutEffect(() => { });
    }
}
