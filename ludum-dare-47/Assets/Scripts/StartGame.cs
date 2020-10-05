using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private float waitBeforeStartGame = 5f;
    private GameData _gameData;

    [Inject]
    public void Construct(GameData gameData)
    {
        _gameData = gameData;
    }

    void Start()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(waitBeforeStartGame);
        _gameData.pauseCounter = false;
    }
}
