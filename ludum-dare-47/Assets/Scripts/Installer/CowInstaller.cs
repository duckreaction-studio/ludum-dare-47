using UnityEngine;
using Zenject;

public class CowInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject CowPrefab;


    public override void InstallBindings()
    {
        Container.BindFactory<string, Vector3, CowSpawner, Cow, Cow.Factory>().FromMonoPoolableMemoryPool(
            x => x.WithInitialSize(10).FromComponentInNewPrefab(CowPrefab).UnderTransformGroup("CowPool")
        );
    }
}