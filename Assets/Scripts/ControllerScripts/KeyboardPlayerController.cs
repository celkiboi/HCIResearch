using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPlayerController : IPlayerController
{
    public static KeyboardPlayerController Instance = new();

    private KeyboardPlayerController() { }

    public Action<int> UpdateHighScore => HighScores.Instance.AddKeyboardScore;

    public bool WantsToDuck()
    {
        return Input.GetKey(KeyCode.DownArrow);
    }

    public bool WantsToGoMainMenu()
    {
        return Input.GetKeyDown(KeyCode.M);
    }

    public bool WantsToJump()
    {
        return Input.GetKey(KeyCode.UpArrow);
    }

    public bool WantsToRestart()
    {
        return Input.GetKeyDown(KeyCode.R);
    }
}
