using OpenCvSharp;
using System;
using System.IO;
using UnityEngine;

public class FaceDetectionController : IPlayerController
{
    public static IPlayerController Instance = new FaceDetectionController();

    private FaceDetectionController() 
    { }

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

    // TODO: This is depreceted, delete it in the interface when refactoring
    public bool WantsToStopDucking()
    {
        return false;
    }
}

