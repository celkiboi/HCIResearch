using OpenCvSharp;
using System;
using System.IO;
using UnityEngine;

public class FaceDetectionController : IPlayerController
{
    public static IPlayerController Instance = new FaceDetectionController();

    private FaceDetectionController()
    { }

    public static int DuckThreshold { get; private set; } = 280;
    public static int JumpThreshold { get; private set; } = 160;

    public static void SetThreshold(int duck, int jump)
    {
        JumpThreshold = jump;
        DuckThreshold = duck;
    }

    public bool WantsToDuck()
    {
        return FaceDetector.Height >= DuckThreshold;
    }

    public bool WantsToGoMainMenu()
    {
        return Input.GetKeyDown(KeyCode.M);
    }

    public bool WantsToJump()
    {
        return FaceDetector.Height <= JumpThreshold;
    }

    public bool WantsToRestart()
    {
        return Input.GetKeyDown(KeyCode.R);
    }
}

