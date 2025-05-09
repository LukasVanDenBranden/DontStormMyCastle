using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private List<Image> _images;
    [SerializeField] private Button _restartButton;

    private Vector3 _player1ImagePosition;
    private Vector3 _player2ImagePosition;
    private void Start()
    {
        _restartButton.onClick.AddListener(OnClick);
        foreach(Image img in _images)
        {
            img.gameObject.SetActive(false);
        }
        _restartButton.gameObject.SetActive(false);

        _player1ImagePosition = _images[0].transform.position;
        _player2ImagePosition = _images[1].transform.position;
    }
    void Update()
    {
        if (GameStateScript._runnerwon)
        {
            _images[1].transform.position = _player1ImagePosition;
            _images[0].transform.position = _player2ImagePosition;

        }

        if (GameStateScript._defenderwon)
        {
            _images[1].transform.position = _player1ImagePosition;
            _images[0].transform.position = _player2ImagePosition;
        }
    }


    private void OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
