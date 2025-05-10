using UnityEngine;

public class GameStateScript : MonoBehaviour
{
    [SerializeField] UiScript _uiScript;
    private int _p1Hearts;
    private bool _runnerwon;
    public enum GameState
    {
        TitleScreen,
        Playing,
        GameOver
    }
    public GameState CurrentState;
    void Start()
    {
        CurrentState = GameState.TitleScreen;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (CurrentState == GameState.TitleScreen) // on start
        {
            if (_uiScript._startButtonPressed == true)
            {
                Time.timeScale = 1;
                _p1Hearts = P1Health.HeartsRemaining;
                _uiScript.OnGameStart();
                CurrentState = GameState.Playing;
            }
        }
        else if (CurrentState == GameState.Playing) // while playing
        {
            if (P1Health.HeartsRemaining <= 0)
            {
                Time.timeScale = 0;
                _uiScript.ActivateGameOverScreen();
                CurrentState = GameState.GameOver;
            }
        }
        else // Game Over
        {
            if (_uiScript._restartButtonPressed == true)
            {
                Time.timeScale = 1;
                P1Health.HeartsRemaining = _p1Hearts;
                _uiScript.OnGameStart();
                CurrentState = GameState.Playing;
            }
        }
    }

    private void OnGUI()
    {
        if (CurrentState == GameState.Playing)
        {
            _uiScript.DrawHearts();
        }
        else if (CurrentState == GameState.GameOver)
        {
            _uiScript.DrawWinAndLoss(_runnerwon);
        }
    }
}
