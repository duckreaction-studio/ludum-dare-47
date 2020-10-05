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

public class SceneTransitionEffect : MonoBehaviour
{
    [SerializeField]
    private RawImage image;
    [SerializeField]
    private float fadeDuration = 1f;

    [Inject]
    public void Construct()
    {
    }

    public void StartFadeInEffect(Action callback)
    {
        var sequence = new SequencedAnimation()
                    .Fade(image, 0, 0, 0.01f) //issue
                    .FadeTo(image, 1, fadeDuration)
                    .Invoke(callback);

        sequence.Play();
    }

    public void StartFadeOutEffect(Action callback)
    {
        var sequence = new SequencedAnimation()
                    .FadeTo(image, 0, fadeDuration)
                    .Invoke(callback);

        sequence.Play();
    }
}
