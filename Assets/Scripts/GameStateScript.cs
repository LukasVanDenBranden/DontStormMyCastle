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
    [SerializeField] public float StartTimeDuration = 6;
    [SerializeField] private AudioSource _countdown;
    [SerializeField] private Image _oneImage;
    [SerializeField] private Image _twoImage;
    [SerializeField] private Image _treeImage;
    [SerializeField] private Image _goImage;
    public float StartTimer;
    public bool _runnerwon; // when the game starts this will always say false only when it's over will this be changed
    public enum GameState
    {
        TitelScreen,
        Playing,
        GameOver
    }
    private GameState CurrentState;
    private bool _countdownHasStarted;
    private bool _countdownHasEnded;

    void Start()
    {
        StartTimer = StartTimeDuration;
        Instance = this;
        uiScript = FindFirstObjectByType<UiScript>();
        CurrentState = GameState.TitelScreen;
        Runner = FindFirstObjectByType<P1Controller>().gameObject;
    }

    private void Update()
    {
        StartCountDown();
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

            if (P1Health.Instance.GetHearths() <= 0 || Runner.transform.position.y <= -17.5f)
            {
                Time.timeScale = 0;
                uiScript.ActivateGameOverScreen();
                CurrentState = GameState.GameOver;

                //when game over vibrate controllers
                GamepadManager.Instance.RumbleController(1, 0.3f, 0.05f);
                GamepadManager.Instance.RumbleController(2, 0.3f, 0.05f);
            }

            if (Runner.transform.position.z > 55 && Locks.index >= 3)
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

    private void StartCountDown()
    {
        if (_countdownHasEnded) return;
        StartTimer -= Time.deltaTime;
        if (StartTimer < 4 && !_countdownHasStarted)
        {
            _countdownHasStarted = true;
            _countdown.Play();
        }
        if (StartTimer <= 4 && StartTimer > 3)
        {
            _treeImage.enabled = true;
        }
        if (StartTimer <= 3 && StartTimer > 2)
        {
            _treeImage.enabled = false;
            _twoImage.enabled = true;
        }
        if (StartTimer <= 2 && StartTimer > 1)
        {
            _twoImage.enabled = false;
            _oneImage.enabled = true;
        }
        if (StartTimer <= 1 && StartTimer > 0)
        {
            _oneImage.enabled = false;
            _goImage.enabled = true;
        }
        if (StartTimer < 0)
        {
            _goImage.enabled = false;
            _countdownHasEnded = true;
        }
    }

    public GameState GetGameState() => CurrentState;
    public bool IsRunnerWinner() => _runnerwon;
}
