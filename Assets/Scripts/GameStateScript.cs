using UnityEngine;

public class GameStateScript : MonoBehaviour
{
    [SerializeField] UiScript uiScript;

    private bool _runnerwon;
    public enum GameState
    {
        TitelScreen,
        Playing,
        GameOver
    }
    public GameState CurrentState;
    void Start()
    {
        CurrentState = GameState.TitelScreen;
    }

    private void Update()
    {
        if (CurrentState == GameState.TitelScreen) // on start
        {
            if (uiScript._startButtonPressed == true)
            {
                uiScript.OnGameStart();
                CurrentState = GameState.Playing;
            }
        }
        else if (CurrentState == GameState.Playing) // while playing
        {
            if (P1Health.HeartsRemaining <= 0)
            {
                Time.timeScale = 0;
                uiScript.ActivateGameOverScreen();
                CurrentState = GameState.GameOver;
            }
        }
        else // Game Over
        {
            if (uiScript._restartButtonPressed == true)
            {
                uiScript.OnGameStart();
                CurrentState = GameState.Playing;
            }
        }
    }

    private void OnGUI()
    {
        if (CurrentState == GameState.GameOver)
        {
            uiScript.DrawWinAndLoss(_runnerwon);
        }
    }
}
