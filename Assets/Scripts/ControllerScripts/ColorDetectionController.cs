using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;


public class ColorDetectionController : IPlayerController
{
    public static IPlayerController Instance = new ColorDetectionController();

    private ColorDetectionController() 
    { }
    public static int DuckThreshold { get; private set; } = 330;
    public static int JumpThreshold { get; private set; } = 200;
    public static void SetThreshold(int duck, int jump)
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

    // TODO: This is depreceted, delete it in the interface when refactoring
    public bool WantsToStopDucking()
    {
        return false;
    }
}

