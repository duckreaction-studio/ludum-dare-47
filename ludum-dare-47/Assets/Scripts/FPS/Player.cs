using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Player : MonoBehaviour, IPlayer
{
    private PlayerInputController _playerInputController;
    private PlayerMovementController _movementController;
    private GameData _gameData;

    [Inject]
    public void Construct(GameData gameData)
    {
        _gameData = gameData;
    }

    public void Start()
    {
        _playerInputController = GetComponentInChildren<PlayerInputController>();
        _movementController = GetComponentInChildren<PlayerMovementController>();
        _playerInputController.inputActions.Player.Pause.performed += OnPausePress;
    }

    private void OnPausePress(InputAction.CallbackContext obj)
    {
        if(obj.performed && !_gameData.end)
        {
            _gameData.pause = !_gameData.pause;
        }
    }

    public Vector3 GetPosition()
    {
        return _movementController == null ? transform.position : _movementController.transform.position;
    }
}
