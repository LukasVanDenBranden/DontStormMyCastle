using NUnit.Framework;
using System;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    [SerializeField] Image startScreenImage;
    [SerializeField] Image _runnerwonGameOverScreen;
    [SerializeField] Image _defenderwonGameOverScreen;
    [SerializeField] Button _startButton;
    [SerializeField] Button _restartButton;
    [SerializeField] Image[] _hearts;
    public bool _startButtonPressed;
    public bool _restartButtonPressed;

    void Start()
    {
        _startButtonPressed = false;
        _restartButtonPressed = false;

        _restartButton.gameObject.SetActive(false);
        _runnerwonGameOverScreen.gameObject.SetActive(false);
        _defenderwonGameOverScreen.gameObject.SetActive(false);
        _startButton.onClick.AddListener(OnStartButtonClick);
        _restartButton.onClick.AddListener(OnRestartButtonClick);

        foreach (Image heart in _hearts)
        {
            heart.gameObject.SetActive(false);
        }
    }


    public void OnGameStart()
    {
        _restartButtonPressed = false;
        startScreenImage.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(false);

        _restartButton.gameObject.SetActive(false);
        _runnerwonGameOverScreen.gameObject.SetActive(false);
        _defenderwonGameOverScreen.gameObject.SetActive(false);
        Locks.index = 0;
        GameStateScript._runnerwon = false;
    }

    public void ActivateGameOverScreen()
    {
        _restartButton.gameObject.SetActive(true);
        _runnerwonGameOverScreen.gameObject.SetActive(true);
        _defenderwonGameOverScreen.gameObject.SetActive(true);
        foreach (Image heart in _hearts)
        {
            heart.gameObject.SetActive(false);
        }
    }
    
    public void DrawHearts()
    {
        int health = FindFirstObjectByType<PlayerManager>().P1Health;
        for (int i = 0; i < 5; i++)
        {
            _hearts[i].gameObject.SetActive(i + 1 <= health);
        }
    }

    public void OnStartButtonClick()
    {
        _startButtonPressed = true;
    }
    public void OnRestartButtonClick()
    {
        _restartButtonPressed = true;
    }
}