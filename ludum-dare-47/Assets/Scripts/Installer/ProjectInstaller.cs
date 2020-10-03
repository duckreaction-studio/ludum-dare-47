using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject effectPrefab;
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<StartSceneTransitionEffectSignal>();

        GameObject effect = Container.InstantiatePrefab(effectPrefab);
        if(Application.isPlaying)
            DontDestroyOnLoad(effect);
    }
}