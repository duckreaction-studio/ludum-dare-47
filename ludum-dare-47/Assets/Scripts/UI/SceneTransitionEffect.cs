using DUCK.Tween;
using DUCK.Tween.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartSceneTransitionEffectSignal
{
    public Action callback{get; private set;}

    public StartSceneTransitionEffectSignal()
    {

    }
    public StartSceneTransitionEffectSignal(Action callback)
    {
        this.callback = callback;
    }
}

public class SceneTransitionEffect : MonoBehaviour
{
    [SerializeField]
    private RawImage image;
    [SerializeField]
    private float fadeDuration = 1f;
    [SerializeField]
    private float waitDuration = 0.5f;

    private SignalBus _signalBus;
    private CompositeDisposable _disposables = new CompositeDisposable();
    private StartSceneTransitionEffectSignal _lastSignal;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        Debug.Log("SceneTransitionEffect is injected");
        _signalBus = signalBus;
        _signalBus.GetStream<StartSceneTransitionEffectSignal>()
            .Subscribe(OnReceiveSignal)
            .AddTo(_disposables);
    }

    protected void OnReceiveSignal(StartSceneTransitionEffectSignal obj)
    {
        _lastSignal = obj;
        StartFadeEffect();
    }

    [ContextMenu("Test transition")]
    public void StartFadeEffect()
    {
        var sequence = new SequencedAnimation()
                    .Fade(image, 0, 0, 0.01f) //issue
                    .FadeTo(image, 1, fadeDuration)
                    .Invoke(InvokeCallback)
                    .Wait(waitDuration)
                    .FadeTo(image, 0, fadeDuration);

        sequence.Play();
    }

    private void InvokeCallback()
    {
        if(_lastSignal != null && _lastSignal.callback != null)
        {
            _lastSignal.callback();
        }
    }
}
