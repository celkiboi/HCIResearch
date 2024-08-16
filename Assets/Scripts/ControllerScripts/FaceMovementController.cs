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

    public int Difference { get; private set; } = 100;
    Queue<int> heights = new();
    int queueLength = 400;
    int outlierThreshold = 30;
    int outlierSampleCount = 35;

    private enum DetectedMovement { NoMovement = 0, Upwards = 1, Downwards = 2}
    DetectedMovement latestMovement = DetectedMovement.NoMovement;
    bool isDucking = false;
    bool isJumping = false;

    public Action<int> UpdateHighScore => HighScores.Instance.AddFaceMovementScore;


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
        if (heights.Count > queueLength)
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
        if (heights.Count < queueLength)
        {
            latestMovement = DetectedMovement.NoMovement;
            return;
        }
            
        List<int> heightsList = heights.ToList();
        RemoveOutliers(heightsList);
        int highest = heightsList.Max();
        int lowest = heightsList.Min();
        if (highest - lowest < Difference)
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
        for (int i = outlierSampleCount; i < heights.Count - outlierSampleCount; i++)
        {
            int beforeSampleSum = 0;
            int afterSampleSum = 0;
            for (int j = i - outlierSampleCount; j < i + outlierSampleCount; j++)
            {
                if (j == i) continue;
                if (j < i) beforeSampleSum += heights[j];
                else afterSampleSum += heights[j];
            }
            int beforeSampleAverage = beforeSampleSum / outlierSampleCount;
            int afterSampleAverage = afterSampleSum / outlierSampleCount;
            // NOT an outlier, just a product of movement
            if (Mathf.Abs(beforeSampleAverage - afterSampleAverage) > Difference) 
                continue;

            int average = (beforeSampleAverage + afterSampleAverage) / 2;
            if (Mathf.Abs(heights[i] - average) > outlierThreshold)
            {
                heights[i] = average;
            }
        }
    }
}

