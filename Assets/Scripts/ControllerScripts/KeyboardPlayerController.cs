using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPlayerController : IPlayerController
{
    public bool WantsToDuck()
    {
        return Input.GetKeyDown(KeyCode.DownArrow);
    }

    public bool WantsToJump()
    {
        return Input.GetKeyDown(KeyCode.UpArrow);
    }

    public bool WantsToStopDucking()
    {
        return Input.GetKeyUp(KeyCode.DownArrow);
    }
}
