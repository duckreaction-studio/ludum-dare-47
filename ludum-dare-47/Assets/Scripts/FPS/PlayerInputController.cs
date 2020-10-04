using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public PlayerInput inputActions;
    
    private void Awake()
    {
        inputActions = new PlayerInput();
    }
    private void OnEnable()
    {
        if(inputActions != null)
            inputActions.Enable();
    }
    private void OnDisable()
    {
        if (inputActions != null)
            inputActions.Disable();
    }

}
