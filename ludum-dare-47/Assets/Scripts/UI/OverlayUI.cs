using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OverlayUI : MonoBehaviour
{
    [SerializeField]
    private Image lifeBar;
    [SerializeField]
    private Text score;

    private GameData _gameData;

    [Inject]
    public void Construct(GameData gameData)
    {
        _gameData = gameData;
        lifeBar.fillAmount = 1;
        score.text = "";
    }

    public void Update()
    {
        lifeBar.fillAmount = _gameData.relativeLife;
        score.text = _gameData.score.ToString();
    }
}
