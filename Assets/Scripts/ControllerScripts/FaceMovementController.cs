using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class FaceMovementController : IPlayerController
{
    public static FaceMovementController Instance = new();

    private FaceMovementController() 
    { }

    public int HeightDifferenceThreshold { get => heightDifferenceThreshold; set => heightDifferenceThreshold = value; }
    readonly Queue<int> heights = new();
    [Settings("faceMovementSampleAmount")]
    static int heightSampleAmount = 400;
    [Settings("faceMovementOutlierThreshold")]
    static int outlierThreshold = 20;
    [Settings("faceMovementOutlierSampleCount")]
    static int outlierSampleCount = 50;
    [Settings("faceMovementThreshold")]
    static private int heightDifferenceThreshold = 100;

    private enum DetectedMovement { NoMovement = 0, Upwards = 1, Downwards = 2}
    DetectedMovement latestMovement = DetectedMovement.NoMovement;
    bool isDucking = false;
    bool isJumping = false;

    public Action<int> UpdateHighScore => HighScores.Instance.AddFaceMovementScore;

    public int HeightSampleAmount { get => heightSampleAmount; set => heightSampleAmount = value; }
    public int OutlierThreshold { get => outlierThreshold; set => outlierThreshold = value; }
    public int OutlierSampleCount { get => outlierSampleCount; set => outlierSampleCount = value; }

    public bool WantsToDuck()
    {
        CheckMovement();
        if (latestMovement == DetectedMovement.NoMovement)
        {
            return isDucking;
        }
        if (latestMovement == DetectedMovement.Upwards)
        {
            isDucking = false;
            return false;
        }
        if (latestMovement == DetectedMovement.Downwards && !isJumping)
        {
            isDucking = true;
            return true;
        }
        return false;
    }

    public bool WantsToGoMainMenu()
    {
        return Input.GetKeyDown(KeyCode.M);
    }

    public bool WantsToJump()
    {
        if (latestMovement == DetectedMovement.NoMovement) 
            return isJumping;
        if (latestMovement == DetectedMovement.Downwards)
        {
            isJumping = false;
            return false;
        }
        if (latestMovement == DetectedMovement.Upwards && !isDucking)
        {
            isJumping = true;
            return true;
        }
        return false;
    }

    public bool WantsToRestart()
    {
        return Input.GetKeyDown(KeyCode.R);
    }

    void FilterQueue()
    {
        if (heights.Count > HeightSampleAmount)
            heights.Dequeue();
    }

    private void PushIntoQueue()
    {
        if (FaceDetector.Height == 0)
            return;
        heights.Enqueue((int)FaceDetector.Height);
        FilterQueue();
    }

    private void CheckMovement()
    {
        PushIntoQueue();
        if (heights.Count < HeightSampleAmount)
        {
            latestMovement = DetectedMovement.NoMovement;
            return;
        }
            
        List<int> heightsList = heights.ToList();
        RemoveOutliers(heightsList);
        int highest = heightsList.Max();
        int lowest = heightsList.Min();
        if (highest - lowest < HeightDifferenceThreshold)
        {
            latestMovement = DetectedMovement.NoMovement;
            return;
        }

        int indexOfHighest = heightsList.IndexOf(highest);
        int indexOfLowest = heightsList.IndexOf(lowest);
        // camera heights are reversed, I.E 10 is above 100
        if (indexOfHighest > indexOfLowest)
            latestMovement = DetectedMovement.Downwards;
        else
            latestMovement = DetectedMovement.Upwards;
    }

    private void RemoveOutliers(List<int> heights)
    {
        for (int i = OutlierSampleCount; i < heights.Count - OutlierSampleCount; i++)
        {
            int beforeSampleSum = 0;
            int afterSampleSum = 0;
            for (int j = i - OutlierSampleCount; j < i + OutlierSampleCount; j++)
            {
                if (j == i) continue;
                if (j < i) beforeSampleSum += heights[j];
                else afterSampleSum += heights[j];
            }
            int beforeSampleAverage = beforeSampleSum / OutlierSampleCount;
            int afterSampleAverage = afterSampleSum / OutlierSampleCount;
            // NOT an outlier, just a product of movement
            if (Mathf.Abs(beforeSampleAverage - afterSampleAverage) > HeightDifferenceThreshold) 
                continue;

            int average = (beforeSampleAverage + afterSampleAverage) / 2;
            if (Mathf.Abs(heights[i] - average) > OutlierThreshold)
            {
                heights[i] = average;
            }
        }
    }
}

