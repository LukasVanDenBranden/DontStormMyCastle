using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    [SerializeField] Image _startScreenImage;
    [SerializeField] Image _nextBoulderImage;
    [SerializeField] Image _heldBoulderImage;
    [SerializeField] Image _runnerwonGameOverScreen;
    [SerializeField] Image _defenderwonGameOverScreen;
    [SerializeField] Image _SprintValueImage;
    [SerializeField] Image _sprintIconImage;
    [SerializeField] Button _startButton;
    [SerializeField] Button _restartButton;
    [SerializeField] Image _hearthImage;
    [SerializeField] Transform _hearthContainer;
    public bool _startButtonPressed;
    public bool _restartButtonPressed;
    private List<Image> _hearthImageList;
    void Start()
    {
        _hearthImageList = new List<Image>();

        _startButtonPressed = false;
        _restartButtonPressed = false;

        _restartButton.gameObject.SetActive(false);
        _runnerwonGameOverScreen.gameObject.SetActive(false);
        _defenderwonGameOverScreen.gameObject.SetActive(false);
        _startButton.onClick.AddListener(OnStartButtonClick);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        P1Health.Instance.OnDamage += P1Health_OnDamage;
    }
    private void Update()
    {
        UpdateSprintMeter();
        _nextBoulderImage.sprite = P2Controller.instance.GetBoulderList()[P2Controller.instance.GetNextBoulderIndex()].GetComponent<BoulderBase>().GetBoulderIcon();
        _heldBoulderImage.sprite = P2Controller.instance.GetBoulderList()[P2Controller.instance.GetHeltBoulderIndex()].GetComponent<BoulderBase>().GetBoulderIcon();
    }

    private void UpdateSprintMeter()
    {
        float sprintCooldown = P1Controller.Instance.GetDashCooldown();
        _SprintValueImage.fillAmount = sprintCooldown;
        if (sprintCooldown >= 1)
        {
            _SprintValueImage.color = UnityEngine.Color.green;
        }
        else
        {
            _SprintValueImage.color = UnityEngine.Color.red;

        }
    }

    public void OnGameStart()
    {
        _restartButtonPressed = false;
        _startScreenImage.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(false);

        _restartButton.gameObject.SetActive(false);
        _runnerwonGameOverScreen.gameObject.SetActive(false);
        _defenderwonGameOverScreen.gameObject.SetActive(false);
        Locks.index = 0;
        UpdateHearths();
    }
    private void P1Health_OnDamage(object sender, EventArgs empty)
    {
        UpdateHearths();
    }
    private void DestroyhearthImages()
    {
        foreach (Image transform in _hearthImageList)
        {
            Destroy(transform.gameObject);
        }
        _hearthImageList.Clear();
    }
    private void UpdateHearths()
    {
        DestroyhearthImages();

        for (int i = 0; i < P1Health.Instance.GetHearths(); i++)
        {
            Image hearthImnstance = Instantiate(_hearthImage, _hearthContainer);
            _hearthImageList.Add(hearthImnstance);
        }
    }

    public void ActivateGameOverScreen()
    {
        _restartButton.gameObject.SetActive(true);
        _runnerwonGameOverScreen.gameObject.SetActive(true);
        _defenderwonGameOverScreen.gameObject.SetActive(true);
        _hearthImageList.Clear();
    }


    public void OnStartButtonClick()
    {
        _startButtonPressed = true;
    }
    public void OnRestartButtonClick()
    {
        _restartButtonPressed = true;
    }
    private void OnDestroy()
    {
        UpdateHearths();
    }
}