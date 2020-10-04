using UnityEngine;
using Zenject;

public class TestCowInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IPlayer>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<CowAI>().FromComponentsInHierarchy().AsSingle();
    }
}