using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private Text textField;

    private int currentScore = 0;

    private SignalBus _signalBus;
    private CompositeDisposable _disposables = new CompositeDisposable();

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        Debug.Log("Score ui is injected");
        _signalBus = signalBus;
        _signalBus.GetStream<IncreaseScoreSignal>()
            .Subscribe(x =>
            {
                currentScore += x.score;
                textField.text = currentScore.ToString();
            })
            .AddTo(_disposables);
    }

    void Start()
    {
        textField.text = "";
    }

    public void OnDestroy()
    {
        _disposables.Dispose();
    }
}
