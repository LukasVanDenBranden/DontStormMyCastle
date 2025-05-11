using Unity.VisualScripting;
using UnityEngine;

public class GameStateScript : MonoBehaviour
{
    private UiScript uiScript;

    private GameObject Runner;

    public static bool _runnerwon; // when the game starts this will always say false only when it's over will this be changed
    public enum GameState
    {
        TitelScreen,
        Playing,
        GameOver
    }
    private GameState CurrentState;
    void Start()
    {
        uiScript = FindFirstObjectByType<UiScript>();
        CurrentState = GameState.TitelScreen;
        Runner = FindFirstObjectByType<P1Controller>().gameObject;
    }

    private void Update()
    {
        if (CurrentState == GameState.TitelScreen) // on start
        {
            if (uiScript._startButtonPressed == true)
            {
                Time.timeScale = 1;
                uiScript.OnGameStart();
                AudioManager.Instance.PlayMusic();
                CurrentState = GameState.Playing;
            }
        }
        else if (CurrentState == GameState.Playing) // while playing
        {

            if (P1Health.Instance.GetHearths()<= 0 || Runner.transform.position.y <= -20)
            {
                Time.timeScale = 0;
                uiScript.ActivateGameOverScreen();
                CurrentState = GameState.GameOver;
            }

            if (Runner.transform.position.z > 45 && Locks.index >= 3)
            {
                Time.timeScale = 0;
                _runnerwon = true;
                uiScript.ActivateGameOverScreen();
                CurrentState = GameState.GameOver;
            }
        }
        else // Game Over
        {
            if (uiScript._restartButtonPressed == true)
            {
                AudioManager.Instance.StopMusic();
                uiScript.OnGameStart();
                CurrentState = GameState.Playing;
            }
        }
    }

}
