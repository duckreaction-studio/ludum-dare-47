using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    PlayerMovementController _movementController;

    public void Start()
    {
        _movementController = GetComponentInChildren<PlayerMovementController>();
    }

    public Vector3 GetPosition()
    {
        return _movementController == null ? transform.position : _movementController.transform.position;
    }
}
