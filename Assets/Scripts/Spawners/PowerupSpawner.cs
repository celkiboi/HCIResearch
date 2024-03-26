using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField]
    int spawnIntervalScore = 250;
    public bool PowerupActive { get; set; }
    public int LastPowerupEndScore { get; set; } = 0;

    [SerializeField]
    Transform lowSpawnPoint;
    [SerializeField]
    Transform highSpawnPoint;

    public bool AllowedToSpawn { get; set; } = true;

    [SerializeField]
    GameObject ghostPowerUp;
    [SerializeField]
    GameObject goldCoinPowerUp;
    [SerializeField]
    GameObject silverCoinPowerUp;

    [SerializeField]
    HUDUpdater hudUpdater;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameManager gameManagerInstance;

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

            Transform spawnPoint;
            if (Random.Range(0,2) == 0)
            {
                spawnPoint = lowSpawnPoint;
            }
            else
            {
                spawnPoint = highSpawnPoint;
            }

            SpawnPowerup(powerUpToSpawn, spawnPoint);
            AllowedToSpawn = false;
        }
    }

    private void SpawnPowerup(GameObject powerUpToSpawn, Transform spawnPoint)
    {
        GameObject obj = Instantiate(powerUpToSpawn, spawnPoint.position, spawnPoint.rotation);
        IPowerUp powerUp = obj.GetComponent<IPowerUp>();
        powerUp.HUDUpdater = hudUpdater;
        powerUp.PowerupSpawner = this;
        powerUp.Player = player;
        powerUp.GameManagerInstance = gameManagerInstance;
    }
}
