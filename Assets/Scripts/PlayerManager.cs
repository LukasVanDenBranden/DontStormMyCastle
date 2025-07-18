using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [SerializeField] private GameObject _p1Prefab;
    [SerializeField] private GameObject _p2Prefab;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 0;

        var gamepads = Gamepad.all; //get all controllers
        var keyboard = Keyboard.current; //get keyboard

        //players need to spawn this way cuz of the connection to their respective controller
        if (gamepads.Count >= 2)
        {
            // Two gamepads connected
            PlayerInput.Instantiate(_p1Prefab, playerIndex: 0, controlScheme: "GamePlay", splitScreenIndex: -1, pairWithDevice: gamepads[0]);
            PlayerInput.Instantiate(_p2Prefab, playerIndex: 1, controlScheme: "GamePlay", splitScreenIndex: -1, pairWithDevice: gamepads[1]);
            if (GamepadManager.Instance == null)
                GamepadManager.Instance = FindFirstObjectByType<GamepadManager>();
            GamepadManager.Instance.P1GamePad = gamepads[0];
            GamepadManager.Instance.P2GamePad = gamepads[1];
        }
        else if (gamepads.Count == 1 && keyboard != null)
        {
            // One gamepad and keyboard connected
            PlayerInput.Instantiate(_p1Prefab, playerIndex: 0, controlScheme: "GamePlay", splitScreenIndex: -1, pairWithDevice: keyboard);
            PlayerInput.Instantiate(_p2Prefab, playerIndex: 1, controlScheme: "GamePlay", splitScreenIndex: -1, pairWithDevice: gamepads[0]);
            if (GamepadManager.Instance == null)
                GamepadManager.Instance = FindFirstObjectByType<GamepadManager>();
            GamepadManager.Instance.P2GamePad = gamepads[0];
        }
        else
        {
            PlayerInput.Instantiate(_p1Prefab, playerIndex: 0, controlScheme: "GamePlay", splitScreenIndex: -1, pairWithDevice: keyboard);
            PlayerInput.Instantiate(_p2Prefab, playerIndex: 1, controlScheme: "GamePlay", splitScreenIndex: -1, pairWithDevice: keyboard);
        }
    }
}
