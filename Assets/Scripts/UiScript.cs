using JetBrains.Annotations;
using System;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    [SerializeField] Image startScreenImage;
    [SerializeField] Texture2D _winImage; //Texture2D because position is variable
    [SerializeField] Texture2D _lossImage;
    [SerializeField] Button _startButton;
    [SerializeField] Button _restartButton;

    [SerializeField] Rect _runnerWinLossRectangle;
    [SerializeField] Rect _defenderWinLossRectangle;

    public bool _startButtonPressed;
    public bool _restartButtonPressed;

    void Start()
    {
        _startButtonPressed = false;
        _restartButtonPressed = false;

        _restartButton.gameObject.SetActive(false);

        _startButton.onClick.AddListener(OnStartButtonClick);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }


    public void OnGameStart()
    {
        _restartButtonPressed = false;
        startScreenImage.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(false);

        _restartButton.gameObject.SetActive(false);
    }

    public void ActivateGameOverScreen()
    {
        _restartButton.gameObject.SetActive(true);
    }
    

    public void OnStartButtonClick()
    {
        _startButtonPressed = true;
    }

    public void OnRestartButtonClick()
    {
        _restartButtonPressed = true;
    }

    public void DrawWinAndLoss(bool didRunnerwin)
    {
        if (didRunnerwin)
        {
            GUI.DrawTexture(_runnerWinLossRectangle,_winImage);
            GUI.DrawTexture(_defenderWinLossRectangle, _lossImage);
            return;
        }
        GUI.DrawTexture(_runnerWinLossRectangle, _lossImage);
        GUI.DrawTexture(_defenderWinLossRectangle, _winImage);
    }
}