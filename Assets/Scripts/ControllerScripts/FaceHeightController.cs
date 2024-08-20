﻿using OpenCvSharp;
using System;
using System.IO;
using UnityEngine;

public class FaceHeightController : IPlayerController
{
    public static FaceHeightController Instance = new();

    private FaceHeightController()
    { }

    public int DuckThreshold { get; private set; } = 280;
    public int JumpThreshold { get; private set; } = 160;

    public Action<int> UpdateHighScore => HighScores.Instance.AddFaceHeightScore;

    public void SetThreshold(int duck, int jump)
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
