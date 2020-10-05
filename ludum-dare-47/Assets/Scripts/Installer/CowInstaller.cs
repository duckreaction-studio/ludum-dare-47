using UnityEngine;
using Zenject;
using Zenject.Internal;

public class CowInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject CowPrefab;

    [Preserve]
    public override void InstallBindings()
    {
        Container.Bind<IPlayer>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<ICowArea>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CowSpawner>().FromComponentInHierarchy().AsSingle();

        Container.BindFactory<string, Vector3, Cow, Cow.Factory>().FromPoolableMemoryPool<string, Vector3, Cow, Cow.Pool>(
            x => x.WithInitialSize(10).FromComponentInNewPrefab(CowPrefab).UnderTransformGroup("CowPool")
        );
    }
}