using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    [Inject]
    private ZenjectSceneLoader loader;

    public void LoadScene()
    {
        loader.LoadSceneAsync(sceneName, loadMode: UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
