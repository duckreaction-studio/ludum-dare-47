using UnityEngine;
using Zenject;

public class TestSceneUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("Ui scene binding");
    //    SignalBusInstaller.Install(Container);
    }
}