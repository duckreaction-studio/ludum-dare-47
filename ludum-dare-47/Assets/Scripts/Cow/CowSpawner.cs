using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CowSpawner : MonoBehaviour
{
    [SerializeField]
    int cowStartCount = 10;
    [SerializeField]
    float delayBetweenNewCow = 5;

    float lastSpawn;
    int cowId = 0;
    Cow.Factory _cowFactory;
    List<Cow> _activeCowList = new List<Cow>();

    [Inject]
    public void Construct(Cow.Factory cowFactory)
    {
        _cowFactory = cowFactory;

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

    public void AddCow()
    {
        ++cowId;
        var cow = _cowFactory.Create("Cow"+cowId,
            GetRandomStartPoint(), this);

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
}
