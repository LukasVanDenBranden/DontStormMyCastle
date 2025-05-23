using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameStateScript : MonoBehaviour
{
    private UiScript uiScript;
    public static GameStateScript Instance;
    private GameObject Runner;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _startButton;

    public bool _runnerwon; // when the game starts this will always say false only when it's over will this be changed
    public enum GameState
    {
        TitelScreen,
        Playing,
        GameOver
    }
    private GameState CurrentState;
    void Start()
    {
        Instance = this;
        uiScript = FindFirstObjectByType<UiScript>();
        CurrentState = GameState.TitelScreen;
        Runner = FindFirstObjectByType<P1Controller>().gameObject;
    }

    private void Update()
    {
        if (CurrentState == GameState.TitelScreen) // on start
        {
            _eventSystem.SetSelectedGameObject(_startButton.gameObject);

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

            if (P1Health.Instance.GetHearths() <= 0 || Runner.transform.position.y <= -20)
            {
                Time.timeScale = 0;
                uiScript.ActivateGameOverScreen();
                CurrentState = GameState.GameOver;

                //when game over vibrate controllers
                GamepadManager.Instance.RumbleController(1, 0.3f, 0.05f);
                GamepadManager.Instance.RumbleController(2, 0.3f, 0.05f);
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
            _eventSystem.SetSelectedGameObject(_restartButton.gameObject);

            if (uiScript._restartButtonPressed == true)
            {
                AudioManager.Instance.StopMusic();
                uiScript.OnGameStart();
                CurrentState = GameState.Playing;
            }
        }
    }
    public GameState GetGameState() => CurrentState;
    public bool IsRunnerWinner() => _runnerwon;
}
