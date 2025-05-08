using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private List<Image> _images;
    [SerializeField] private Button _restartButton;
    private void Start()
    {
        _restartButton.onClick.AddListener(OnClick);
        foreach(Image img in _images)
        {
            img.gameObject.SetActive(false);
        }
        _restartButton.gameObject.SetActive(false);
    }
    void Update()
    {
        if (P1Health.HeartsRemaining <= 0)
        {
            foreach (Image img in _images)
            {
                img.gameObject.SetActive(true);
            }
            _restartButton.gameObject.SetActive(true);
        }
    }
    private void OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
