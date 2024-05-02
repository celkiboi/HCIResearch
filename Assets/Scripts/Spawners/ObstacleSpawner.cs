using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject lowObstaclePrefab;
    [SerializeField]
    GameObject mediumObstaclePrefab;
    [SerializeField]
    GameObject highObstaclePrefab;
    [SerializeField]
    GameObject tallObstaclePrefab;
    [SerializeField]
    Transform lowSpawnPoint;
    [SerializeField]
    Transform middleSpawnPoint;
    [SerializeField]
    Transform highSpawnPoint;
    [SerializeField]
    Transform tallSpawnPoint;

    readonly Transform[] spawnPoints = new Transform[4];
    readonly GameObject[] obstacles = new GameObject[4];

    [SerializeField]
    PlayerBehaviour playerBehaviour;

    public bool CanSpawn = false;

    readonly System.Random random = new();

    private void Start()
    {
        spawnPoints[0] = lowSpawnPoint;
        spawnPoints[1] = middleSpawnPoint;
        spawnPoints[2] = highSpawnPoint;
        spawnPoints[3] = tallSpawnPoint;

        obstacles[0] = lowObstaclePrefab;
        obstacles[1] = mediumObstaclePrefab;
        obstacles[2] = highObstaclePrefab;
        obstacles[3] = tallObstaclePrefab;
    }

    /*
     * The odds for spawning an obstacle for jumping is 50%
     * Since all remaining obstacles can be ducked under, their total spawn rate is 50%
     */
    private void FixedUpdate()
    {
        if (CanSpawn) 
        {
            if (random.Next(0, 2) == 0) 
            {
                Instantiate(obstacles[0], spawnPoints[0]);
                CanSpawn = false;
                return;
            }
            int choice = random.Next(1, 4);
            Instantiate(obstacles[choice], spawnPoints[choice]);
            CanSpawn = false;
        }
    }
}
