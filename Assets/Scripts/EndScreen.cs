using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Image _winImages;
    [SerializeField] private Image _LoseImages;
    [SerializeField] private Button _restartButton;


    [SerializeField] private Transform _p1WinLoseStartPosition;
    [SerializeField] private Transform _p2WinLoseStartPosition;

    private void Start()
    {
        _restartButton.onClick.AddListener(OnClick);
        _winImages.gameObject.SetActive(false);
        _LoseImages.gameObject.SetActive(false);


        _restartButton.gameObject.SetActive(false);
    }
    void Update()
    {
        if (GameStateScript.Instance.GetGameState() != GameStateScript.GameState.GameOver) return;

        _winImages.gameObject.SetActive(true);
        _LoseImages.gameObject.SetActive(true);

        if (GameStateScript.Instance.IsRunnerWinner())
        {
            _winImages.transform.position = _p1WinLoseStartPosition.position;
            _LoseImages.transform.position = _p2WinLoseStartPosition.position;
        }
        else
        {
            _winImages.transform.position = _p2WinLoseStartPosition.position;
            _LoseImages.transform.position = _p1WinLoseStartPosition.position;
        }

    }


    private void OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
