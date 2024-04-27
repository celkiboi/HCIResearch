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
    Transform middleSpawnPoint;
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

    readonly Transform[] spawnPoints = new Transform[3];

    private void Start()
    {
        spawnPoints[0] = lowSpawnPoint;
        spawnPoints[1] = middleSpawnPoint;
        spawnPoints[2] = highSpawnPoint;
    }

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

            int choice = Random.Range(0, 3);
            SpawnPowerup(powerUpToSpawn, spawnPoints[choice]);
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
