using Sound;
using UnityEngine;
using Zenject;
using Zenject.Internal;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject effectPrefab;
    [SerializeField]
    private GameObject soundManagerPrefab;

    [Preserve]
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<GameOver>().OptionalSubscriber();
        Container.DeclareSignal<GameRestart>().OptionalSubscriber();

        Container.BindInterfacesAndSelfTo<GameData>().AsSingle().NonLazy();

        Container.Bind<ISoundManager>().FromComponentInNewPrefab(soundManagerPrefab).AsSingle();

        Container.Bind<SceneTransitionEffect>().FromComponentInNewPrefab(effectPrefab).AsSingle().NonLazy();
    }
}