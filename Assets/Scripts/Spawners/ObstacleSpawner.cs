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

    private void FixedUpdate()
    {
        if (CanSpawn) 
        {
            int choice = Random.Range(0, 4);
            Instantiate(obstacles[choice], spawnPoints[choice]);
            CanSpawn = false;
        }
    }
}
