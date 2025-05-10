using JetBrains.Annotations;
using System;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    [SerializeField] int _heartImageDistance;
    [SerializeField] Image startScreenImage;
    [SerializeField] Texture2D _winImage; //Texture2D because position is variable
    [SerializeField] Texture2D _lossImage;
    [SerializeField] Texture2D _heartTexture;
    [SerializeField] Button _startButton;
    [SerializeField] Button _restartButton;

    [SerializeField] Rect _runnerWinLossRectangle;
    [SerializeField] Rect _defenderWinLossRectangle;
    [SerializeField] Rect _firstHeartRect; // "first" because the other two will be based on this rectangle position
    private Rect _secondHeartRect;
    private Rect _thirdHeartRect;

    public bool _startButtonPressed;
    public bool _restartButtonPressed;

    void Start()
    {
        _startButtonPressed = false;
        _restartButtonPressed = false;

        _restartButton.gameObject.SetActive(false);

        _startButton.onClick.AddListener(OnStartButtonClick);
        _restartButton.onClick.AddListener(OnRestartButtonClick);

        _secondHeartRect = _firstHeartRect;
        _secondHeartRect.x += _heartImageDistance;
        _thirdHeartRect = _secondHeartRect;
        _thirdHeartRect.x += _heartImageDistance;
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

    public void DrawGameUI()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        int hearts = P1Health.HeartsRemaining;
        GUI.DrawTexture(_firstHeartRect, _heartTexture);
        if(hearts > 1)
        {
            GUI.DrawTexture(_secondHeartRect, _heartTexture);
            if (hearts == 3)
            {
                GUI.DrawTexture(_thirdHeartRect, _heartTexture);
            }
        }
    }
}