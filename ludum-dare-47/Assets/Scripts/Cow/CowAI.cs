using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class CowAI : MonoBehaviour
{
    NavMeshAgent _navAgent;
    IPlayer _player;

    public void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    [Inject]
    public void Construct(IPlayer player)
    {
        _player = player;
    }

    public void StartFollowPlayer()
    {
        _navAgent.enabled = true;
        _navAgent.SetDestination(_player.GetPosition());
    }
}
