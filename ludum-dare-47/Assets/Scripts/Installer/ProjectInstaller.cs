using Sound;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject effectPrefab;
    [SerializeField]
    private GameObject soundManagerPrefab;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<GameOver>();
        Container.DeclareSignal<GameRestart>();
        Container.DeclareSignal<StartSceneTransitionEffectSignal>();

        Container.BindInterfacesAndSelfTo<GameData>().AsSingle();

        Container.Bind<ISoundManager>().FromComponentInNewPrefab(soundManagerPrefab).AsSingle();

        GameObject effect = Container.InstantiatePrefab(effectPrefab);
        if(Application.isPlaying)
            DontDestroyOnLoad(effect);
    }
}