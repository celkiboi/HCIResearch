using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class ColorDetectionController : IPlayerController
{
    public static ColorDetectionController Instance = new();

    private ColorDetectionController() 
    { }
    public int DuckThreshold { get; private set; } = 280;
    public int JumpThreshold { get; private set; } = 160;

    public Action<int> UpdateHighScore => HighScores.Instance.AddColorScore;

    public void SetThreshold(int duck, int jump)
    {
        JumpThreshold = jump;
        DuckThreshold = duck;
    }

    public bool WantsToDuck()
    {
        return ColorDetector.Height >= DuckThreshold;
    }

    public bool WantsToGoMainMenu()
    {
        return Input.GetKeyDown(KeyCode.M);
    }

    public bool WantsToJump()
    {
        return ColorDetector.Height <= JumpThreshold;
    }

    public bool WantsToRestart()
    {
        return Input.GetKeyDown(KeyCode.R);
    }
}

