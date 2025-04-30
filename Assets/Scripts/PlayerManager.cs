using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _p1Prefab;
    [SerializeField] private GameObject _p2Prefab;

    private void Start()
    {
        var gamepads = Gamepad.all; //get all controllers
        //assign controller
        PlayerInput.Instantiate(_p1Prefab, playerIndex: 0, controlScheme: "GamePlay", splitScreenIndex: -1, pairWithDevice: gamepads[0]);
        PlayerInput.Instantiate(_p2Prefab, playerIndex: 1, controlScheme: "GamePlay", splitScreenIndex: -1, pairWithDevice: gamepads[1]);
    }
}
