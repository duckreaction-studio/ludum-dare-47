using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class TestSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<IncreaseScoreSignal>();

        Container.BindSignal<IncreaseScoreSignal>()
            .ToMethod(x => Debug.Log("IncreaseScore "+x.score));

    }
}