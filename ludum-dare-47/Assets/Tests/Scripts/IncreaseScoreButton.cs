using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class IncreaseScoreSignal
{
    public int score { get; set; }

    public IncreaseScoreSignal(int score)
    {
        this.score = score; 
    }
}

public class IncreaseScoreButton : MonoBehaviour
{
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        Debug.Log("I'm injected");
        _signalBus = signalBus;
    }

    public void OnValidatePerformed(InputAction.CallbackContext obj)
    {
        if(obj.performed)
            OnClick();
    }

    public void OnClick()
    {
        _signalBus.Fire(new IncreaseScoreSignal(10));
    }

}
