using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField]
    int spawnIntervalScore = 250;
    public static bool PowerupActive { get; set; }
    public static int LastPowerupEndScore { get; set; } = 0;

    [SerializeField]
    Transform powerUpSpawnPoint;

    public static bool AllowedToSpawn { get; set; } = true;

    [SerializeField]
    GameObject ghostPowerUp;
    [SerializeField]
    GameObject goldCoinPowerUp;
    [SerializeField]
    GameObject silverCoinPowerUp;

    private void FixedUpdate()
    {
        if ((GameManager.Score - LastPowerupEndScore) > spawnIntervalScore && AllowedToSpawn)
        {
            GameObject powerUpToSpawn;
            switch(Random.Range(0,3))
            {
                case 0:
                    powerUpToSpawn = ghostPowerUp; break;
                case 1:
                    powerUpToSpawn = goldCoinPowerUp; break;
                case 2:
                    powerUpToSpawn = silverCoinPowerUp; break;
                default:
                    return;
            }
            SpawnPowerup(powerUpToSpawn);
            AllowedToSpawn = false;
        }
    }

    private void SpawnPowerup(GameObject powerUp)
    {
        GameObject gameObject = Instantiate(powerUp, powerUpSpawnPoint);
        gameObject.GetComponent<IPowerUp>().Player = GameObject.Find("Player");
    }
}
