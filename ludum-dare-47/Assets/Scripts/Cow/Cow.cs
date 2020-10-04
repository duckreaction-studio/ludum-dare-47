using Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Cow : MonoBehaviour, IPoolable<string, Vector3, IMemoryPool>, IDisposable
{
    [SerializeField]
    private ParticleSystem explosion;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Transform body;
    [SerializeField]
    private float waitAfterDie = 2f;
    [SerializeField]
    private float minBeforeNextSound = 10f;
    [SerializeField]
    private float maxBeforeNextSound = 20f;

    private GameData _gameData;
    private CowSpawner _spawner;
    private ISoundManager _soundManger;
    private IMemoryPool _pool;
    private float waitBeforeNextSound;

    public void Awake()
    {
        explosion.gameObject.SetActive(false);
    }

    [Inject]
    public void Construct(GameData gameData, CowSpawner spawner, ISoundManager soundManager)
    {
        _gameData = gameData;
        _spawner = spawner;
        _soundManger = soundManager;

        UpdateNextSoundRandomTime();
    }

    public void Update()
    {
        if(_gameData.running && Time.realtimeSinceStartup > waitBeforeNextSound)
        {
            _soundManger.PlaySound("meuh", audioSource);
            UpdateNextSoundRandomTime();
        }
    }

    private void UpdateNextSoundRandomTime()
    {
        waitBeforeNextSound = UnityEngine.Random.Range(minBeforeNextSound, maxBeforeNextSound) + Time.realtimeSinceStartup;
    }

    public void Die()
    {
        _gameData.PlayerCatchACow();

        body.gameObject.SetActive(false);
        explosion.gameObject.SetActive(true);
        explosion.loop = false;
        explosion.Play();

        StartCoroutine(AfterDieAnimation());
    }

    private IEnumerator AfterDieAnimation()
    {
        yield return new WaitForSeconds(waitAfterDie);
        _spawner.RemoveCow(this);
    }

    public void OnSpawned(string name, Vector3 position, IMemoryPool pool)
    {
        _pool = pool;
        gameObject.name = name;
        transform.position = position;

        Reset();
    }

    public void Reset()
    {
        body.gameObject.SetActive(true);
        explosion.gameObject.SetActive(false);
    }

    public void OnDespawned()
    {
        _pool = null;
    }

    public void Dispose()
    {
        _pool.Despawn(this);
    }

    public class Factory : PlaceholderFactory<string, Vector3, Cow>
    {

    }

}
