using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class CowSpawner : MonoBehaviour
{
    [SerializeField]
    int cowStartCount = 10;
    [SerializeField]
    float delayBetweenNewCow = 5;

    float lastSpawn;
    int cowId = 0;
    Cow.Factory _cowFactory;
    SignalBus _signalBus;
    CompositeDisposable _disposables = new CompositeDisposable();
    List<Cow> _activeCowList = new List<Cow>();

    [Inject]
    public void Construct(Cow.Factory cowFactory, SignalBus signalBus)
    {
        _cowFactory = cowFactory;
        _signalBus = signalBus;
        _signalBus.GetStream<GameOver>()
            .Subscribe(x => DestroyAllCows())
            .AddTo(_disposables);
        _signalBus.GetStream<GameRestart>()
            .Subscribe(x => Restart())
            .AddTo(_disposables);

        StartCoroutine(CreatFirstCows());
    }

    private IEnumerator CreatFirstCows()
    {
        for(int i = 0; i < cowStartCount; i++)
        {
            AddCow();
            yield return new WaitForSeconds(0.02f);
        }
        lastSpawn = Time.realtimeSinceStartup;
    }

    public void Update()
    {
        if(Time.realtimeSinceStartup - lastSpawn > delayBetweenNewCow)
        {
            AddCow();
            lastSpawn = Time.realtimeSinceStartup;
        }
    }

    public void DestroyAllCows()
    {
        while(_activeCowList.Count > 0)
        {
            RemoveCow(_activeCowList[0]);
        }
    }

    public void Restart()
    {
        StartCoroutine(CreatFirstCows());
    }

    public void AddCow()
    {
        ++cowId;
        var cow = _cowFactory.Create("Cow"+cowId,GetRandomStartPoint());

        _activeCowList.Add(cow);
    }

    private Vector3 GetRandomStartPoint()
    {
        int rand = UnityEngine.Random.Range(0, transform.childCount);
        return transform.GetChild(rand).position;
    }

    public void RemoveCow(Cow cow)
    {
        if(_activeCowList.Contains(cow))
        {
            _activeCowList.Remove(cow);
            cow.Dispose();
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach(Transform child in transform)
        {
            Gizmos.DrawLine(child.position, child.position + Vector3.up * 3);
        }
    }
}
