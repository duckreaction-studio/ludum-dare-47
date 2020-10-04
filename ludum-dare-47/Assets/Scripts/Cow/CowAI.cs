using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public enum CowState
{
    UNKNOWN,
    IDLE,
    WALK,
    RUN_TO_PLAYER
}

public class CowAI : MonoBehaviour
{
    [Header("Sensors")]
    [SerializeField]
    private float playerMinDistance = 10f;
    [SerializeField]
    private float playerFleeDistance = 15f;
    [SerializeField]
    private float destinationMinDistance = 1.5f;
    [SerializeField]
    private float cowReactivity = 1f;

    [Header("Nav agent")]
    [SerializeField]
    private float walkSpeed = 3f;
    [SerializeField]
    private float runSpeed = 5f;

    [Header("FX")]
    [SerializeField]
    private ParticleSystem alert;


    public CowState state { get; private set; } 
    private NavMeshAgent _navAgent;
    private Animator _animator;
    private IPlayer _player;
    private ICowArea _area;
    private GameData _gameData;
    private bool inited;
    private float stateStartTime;
    private float lastPlayerDistanceCheckTime;

    public void Awake()
    {
        playerMinDistance = Mathf.Pow(playerMinDistance, 2f);
        playerFleeDistance = Mathf.Pow(playerFleeDistance, 2f);
        destinationMinDistance = Mathf.Pow(destinationMinDistance, 2f);
    }

    [Inject]
    public void Construct(IPlayer player, ICowArea area, GameData gameData)
    {
        _player = player;
        _area = area;
        _gameData = gameData;
    }

    public void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>(true);
    }

    public void Update()
    {
        if (!inited)
        {
            _navAgent.enabled = true;
            if (_navAgent.isOnNavMesh)
            {
                inited = true;
                state = GetNextState();
                InitCurrentState();
            }
            else
            {
                _navAgent.enabled = false;
            }
        }
        else
        {
            if (_gameData.pause)
            {
                _navAgent.enabled = false;
            }
            else
            {
                _navAgent.enabled = true;
                if (state != CowState.RUN_TO_PLAYER && CheckCowDetectPlayer())
                {
                    state = CowState.RUN_TO_PLAYER;
                    InitCurrentState();
                    return;
                }
                if (CurrentStateIsFinished())
                {
                    state = GetNextState();
                    InitCurrentState();
                }
            }
        }
    }

    private bool CheckCowDetectPlayer()
    {
        if (Time.realtimeSinceStartup - lastPlayerDistanceCheckTime > cowReactivity)
        {
            lastPlayerDistanceCheckTime = Time.realtimeSinceStartup;
            return PlayerIsTooClose();
        }
        return false;
    }

    private bool CurrentStateIsFinished()
    {
        switch(state)
        {
            case CowState.IDLE:
                if (Time.realtimeSinceStartup - stateStartTime > 5f)
                    return true;
                break;
            case CowState.WALK:
                if (CowIsCloseToDestination())
                    return true;
                break;
            case CowState.RUN_TO_PLAYER:
                if (PlayerIsTooFar())
                    return true;
                break;
        }
        return false;
    }

    private CowState GetNextState()
    {
        if(PlayerIsTooClose())
        {
            return CowState.RUN_TO_PLAYER;
        }
        else if(UnityEngine.Random.value > 0.75)
        {
            return CowState.IDLE;
        }
        else
        {
            return CowState.WALK;
        }
    }

    private void InitCurrentState()
    {
        stateStartTime = Time.realtimeSinceStartup;
        switch(state)
        {
            case CowState.RUN_TO_PLAYER:
                StartFollowPlayer();
                break;
            case CowState.WALK:
                StartGotoRandomPosition();
                break;
            default:
                StartIdle();
                break;
        }
    }

    private bool CowIsCloseToDestination()
    {
        var distance = (_navAgent.destination - transform.position).sqrMagnitude;
        return distance < destinationMinDistance;
    }

    private bool PlayerIsTooClose()
    {
        if (_player == null)
            return false;
        float distance = (_player.GetPosition() - transform.position).sqrMagnitude;
        return distance <= playerMinDistance;
    }

    private bool PlayerIsTooFar()
    {
        if (_player == null)
            return true;
        float distance = (_player.GetPosition() - transform.position).sqrMagnitude;
        return distance > playerFleeDistance;
    }

    public void StartIdle()
    {
        AnimatorTrigger("idle");
    }

    public void StartFollowPlayer()
    {
        _navAgent.enabled = true;
        _navAgent.speed = runSpeed;
        _navAgent.SetDestination(_player.GetPosition());

        Debug.Log("Alert");
        if (alert != null)
            alert.Play();
        AnimatorTrigger("run");
    }

    public void StartGotoRandomPosition()
    {
        Vector3 pos = _area.GetRandomPosition();

        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(pos, out closestHit, 500f, NavMesh.AllAreas))
            pos = closestHit.position;
        else
            Debug.LogError("Could not find position on NavMesh!");

        _navAgent.enabled = true;
        _navAgent.speed = walkSpeed;
        _navAgent.SetDestination(pos);

        AnimatorTrigger("walk");
    }

    public void AnimatorTrigger(string trigger)
    {
        if(_animator != null)
        {
            _animator.SetTrigger(trigger);
        }
    }

    public void OnDrawGizmosSelected()
    {
        if(!Application.isPlaying)
        {
            Color color = Color.red;
            color.a = 0.3f;
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, playerMinDistance);

            color = Color.yellow;
            color.a = 0.3f;
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, playerFleeDistance);
        }
        else
        {
            if(state == CowState.WALK || state == CowState.RUN_TO_PLAYER)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_navAgent.destination, 1f);
            }
        }
    }
}
