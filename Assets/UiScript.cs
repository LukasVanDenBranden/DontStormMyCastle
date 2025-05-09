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
    private Vector2 _screenDimensions;
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
    }

    public void ActivateGameOverScreen()
    {
        _restartButton.gameObject.SetActive(true);
        _runnerwonGameOverScreen.gameObject.SetActive(true);
        _defenderwonGameOverScreen.gameObject.SetActive(true);
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