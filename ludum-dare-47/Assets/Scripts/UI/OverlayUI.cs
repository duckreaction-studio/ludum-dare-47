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
    private Text finalScore;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject gamePanel;

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
        if (gamePanel.activeSelf != _gameData.end)
        {
            finalScore.text = "Your score : " + _gameData.score;
            gamePanel.SetActive(_gameData.end);
        }
    }

    public void ResumeGame()
    {
        _gameData.pause = false;
    }

    public void RestartGame()
    {
        _gameData.Restart();
    }
}
