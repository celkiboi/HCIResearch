using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XboxPlayerController : IPlayerController
{
    public static IPlayerController Instance = new XboxPlayerController();

    private XboxPlayerController() { }

    readonly float deadzone = 0.2f;

    public bool WantsToDuck()
    {
        float value = Input.GetAxis("DPadVertical");

        return value < -deadzone;
    }

    public bool WantsToGoMainMenu()
    {
        return Input.GetKeyDown(KeyCode.JoystickButton1);
    }

    public bool WantsToJump()
    {
        float value = Input.GetAxis("DPadVertical");

        return value > deadzone;
    }

    public bool WantsToRestart()
    {
        return Input.GetKeyDown(KeyCode.JoystickButton0);
    }

    public bool WantsToStopDucking()
    {
        float value = Input.GetAxis("DPadVertical");

        return value > -deadzone;
    }
}
