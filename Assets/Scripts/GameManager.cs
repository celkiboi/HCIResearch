using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    ObstacleSpawner obstacleSpawner;
    [SerializeField]
    HUDUpdater hudUpdater;
    [SerializeField]
    BackgroundManager backgroundManager;

    [SerializeField]
    int ticksPerPoint = 10;
    int ticksPassed = 0;
    public static int Score { get; private set; } = 0;
    public static bool IsFinished { get; set; } = false;
    public static float GameSpeed { get; private set; }

    public float GameScoreSpeedModifier = 1f;
    public float BaseGameSpeed = 1f;

    bool hasStarted = false;

    void Start()
    {
        Score = 0;
        GameSpeed = BaseGameSpeed;
        IsFinished = false;
        Time.timeScale = 1;
        StartCoroutine(DoStartCountdown());
    }

    void FixedUpdate()
    {
        UpdateScore();
        GameSpeed = BaseGameSpeed + Score / GameScoreSpeedModifier;
    }

    private void UpdateScore()
    {
        ticksPassed++;
        if (ticksPassed >= ticksPerPoint && hasStarted)
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

    IEnumerator DoStartCountdown()
    {
        hudUpdater.UpdateCountDown(3);
        yield return new WaitForSeconds(1f);
        hudUpdater.UpdateCountDown(2);
        yield return new WaitForSeconds(1f);
        hudUpdater.UpdateCountDown(1);
        yield return new WaitForSeconds(1f);
        hudUpdater.UpdateCountDown(0);
        obstacleSpawner.CanSpawn = true;
        backgroundManager.StartBackgroundMovement();
        hasStarted = true;
    }

}
