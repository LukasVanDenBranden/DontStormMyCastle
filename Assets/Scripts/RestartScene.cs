using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScene : MonoBehaviour
{
    private Button _button;
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Restart);
    }
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
