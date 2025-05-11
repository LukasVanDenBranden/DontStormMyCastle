using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadManager : MonoBehaviour
{
    public static GamepadManager Instance;

    public Gamepad P1GamePad;
    public Gamepad P2GamePad;

    private Coroutine p1RumbleCoroutine;
    private Coroutine p2RumbleCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    //duration is in seconds
    public void RumbleController(int playerNr, float intensity, float duration)
    {
        if (playerNr == 1 && P1GamePad != null)
        {
            //stop previous
            if (p1RumbleCoroutine != null)
                StopCoroutine(p1RumbleCoroutine);

            P1GamePad.SetMotorSpeeds(intensity, intensity);
            p1RumbleCoroutine = StartCoroutine(StopRumbleAfterDelay(playerNr, duration));
        }
        else if (playerNr == 2 && P2GamePad != null)
        {
            //stop previous
            if (p2RumbleCoroutine != null)
                StopCoroutine(p2RumbleCoroutine);

            P2GamePad.SetMotorSpeeds(intensity, intensity);
            p2RumbleCoroutine = StartCoroutine(StopRumbleAfterDelay(playerNr, duration));
        }
    }

    private IEnumerator StopRumbleAfterDelay(int playerNr, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (playerNr == 1)
        {
            P1GamePad.SetMotorSpeeds(0f, 0f);
            p1RumbleCoroutine = null;
        }
        else if (playerNr == 2)
        {
            P2GamePad.SetMotorSpeeds(0f, 0f);
            p2RumbleCoroutine = null;
        }
    }
}
