using OpenCvSharp;
using System;
using System.IO;
using UnityEngine;

public class FaceDetectionController : IPlayerController
{
    public static IPlayerController Instance = new FaceDetectionController();

    private FaceDetectionController() 
    { }

    readonly int duckThreshold = 280;
    readonly int jumpThreshold = 80;

    public bool WantsToDuck()
    {
        return FaceDetector.Height >= duckThreshold;
    }

    public bool WantsToGoMainMenu()
    {
        return Input.GetKeyDown(KeyCode.M);
    }

    public bool WantsToJump()
    {
        return FaceDetector.Height <= jumpThreshold;
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

