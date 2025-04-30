using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LossImage : MonoBehaviour
{
    private List<PlayerController> _playerList;
    [SerializeField] private Image _lossImage;
    [SerializeField] private GameObject _restartButton;
    void Start()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            PlayerController playerscript = obj.GetComponent<PlayerController>();
            playerscript.OnPlayerDestroy += OnPlayerDeath;
            _lossImage.enabled = false;
            _restartButton.SetActive(false);
        }
    }

    private void OnPlayerDeath(object sender, EventArgs empty)
    {
        if(!TryGetComponent<DefendingPlayerController>(out DefendingPlayerController script))
        {
            _lossImage.enabled = true;
            _restartButton.SetActive(true);

        }
    }
}
