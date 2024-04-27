using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject lowObstaclePrefab;
    [SerializeField]
    GameObject highObstaclePrefab;
    [SerializeField]
    Transform lowSpawnPoint;
    [SerializeField]
    Transform middleSpawnPoint;
    [SerializeField]
    Transform highSpawnPoint;

    readonly Transform[] spawnPoints = new Transform[3];
    readonly GameObject[] obstacles = new GameObject[3];

    [SerializeField]
    PlayerBehaviour playerBehaviour;

    public bool CanSpawn = false;

    private void Start()
    {
        spawnPoints[0] = lowSpawnPoint;
        spawnPoints[1] = middleSpawnPoint;
        spawnPoints[2] = highSpawnPoint;

        obstacles[0] = lowObstaclePrefab;
        obstacles[1] = lowObstaclePrefab;
        obstacles[2] = highObstaclePrefab;
    }

    private void FixedUpdate()
    {
        if (CanSpawn) 
        {
            int choice = Random.Range(0, 3);
            Instantiate(obstacles[choice], spawnPoints[choice]);
            CanSpawn = false;
        }
    }
}
