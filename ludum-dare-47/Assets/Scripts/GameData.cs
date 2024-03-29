﻿using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class GameOver
{

}

public class GameRestart
{

}

public class GameData : ITickable
{
    public static readonly int MAX_LIFE = 100;
    public static readonly int SCORE_PER_COW = 100;
    public static readonly int SCORE_PER_SECOND = 10;
    public static readonly int LOOSE_LIFE_RATIO = 4;

    public int score 
    {
        get
        {
            return (int)(_timeScore * SCORE_PER_SECOND + _cowScore * SCORE_PER_COW);
        }
    }
    public float life { get; private set; } = 100;
    public float relativeLife
    {
        get
        {
            return life / MAX_LIFE;
        }
    }

    public bool pauseCounter { get; set; } = true;
    public bool pause { get; set; }
    public bool end { get; private set; }

    public bool running { get { return !pause && !end; } }

    private float _timeScore;
    private int _cowScore;
    private SignalBus _signalBus;

    [Inject]
    public GameData(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Restart()
    {
        life = MAX_LIFE;
        _timeScore = 0;
        _cowScore = 0;
        end = false;
        pause = false;
        _signalBus.Fire<GameRestart>();
    }

    public void Tick()
    {
        if(!pauseCounter && !pause && !end)
        {
            _timeScore += Time.deltaTime;
            life -= Time.deltaTime * LOOSE_LIFE_RATIO;
            if(life <= 0)
            {
                end = true;
                _signalBus.Fire<GameOver>();
            }
        }
    }

    public void PlayerCatchACow()
    {
        _cowScore++;
        life += 10;
        life = Mathf.Min(life, 100);
    }
}
