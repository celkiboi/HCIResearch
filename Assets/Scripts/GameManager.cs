using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int ticksPerPoint = 10;
    int ticksPassed = 0;
    public static int Score { get; private set; } = 0;
    public static bool IsFinished { get; set; }
    public static float GameSpeed { get; private set; }

    public float GameScoreSpeedModifier = 1f;
    public float BaseGameSpeed = 1f;


    void FixedUpdate()
    {
        UpdateScore();
        GameSpeed = BaseGameSpeed + Score / GameScoreSpeedModifier;
    }

    private void UpdateScore()
    {
        ticksPassed++;
        if (ticksPassed >= ticksPerPoint)
        {
            ticksPassed = 0;
            Score++;
        }
        if (IsFinished)
            Time.timeScale = 0;
    }

    public void AddToScore(int value)
    {
        Score += value;
    }

}
