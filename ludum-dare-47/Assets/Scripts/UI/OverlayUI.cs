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
    [SerializeField]
    private GameObject pausePanel;

    private GameData _gameData;

    [Inject]
    public void Construct(GameData gameData)
    {
        _gameData = gameData;
        lifeBar.fillAmount = 1;
        score.text = "";
        pausePanel.SetActive(false);
    }

    public void Update()
    {
        lifeBar.fillAmount = _gameData.relativeLife;
        score.text = _gameData.score.ToString();
        if (pausePanel.activeSelf != _gameData.pause)
            pausePanel.SetActive(_gameData.pause);
    }

    public void ResumeGame()
    {
        _gameData.pause = false;
    }
}
