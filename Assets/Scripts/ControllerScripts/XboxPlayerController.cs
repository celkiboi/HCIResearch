using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XboxPlayerController : IPlayerController
{
    public bool WantsToDuck()
    {
        return Input.GetKeyDown(KeyCode.JoystickButton5);
    }
    public bool WantsToJump()
    {
        return Input.GetKeyDown(KeyCode.JoystickButton4);
    }

    public bool WantsToRestart()
    {
        return Input.GetKeyDown(KeyCode.JoystickButton1);
    }

    public bool WantsToStopDucking()
    {
        return Input.GetKeyUp(KeyCode.JoystickButton5);
    }
}
