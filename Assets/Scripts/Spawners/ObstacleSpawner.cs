using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] 
    float spawnIntervalSeconds = 1.5f;
    [SerializeField]
    GameObject lowObstaclePrefab;
    [SerializeField]
    GameObject highObstaclePrefab;
    [SerializeField]
    Transform lowSpawnPoint;
    [SerializeField]
    Transform highSpawnPoint;

    [SerializeField]
    PlayerBehaviour playerBehaviour;

    public bool CanSpawn = false;

    private void Start()
    {
        StartCoroutine(SpawnEnemiesInIntervals(spawnIntervalSeconds));
    }

    IEnumerator SpawnEnemiesInIntervals(float spawnIntervalSeconds)
    {
        for (;;)
        {
            if (CanSpawn) 
            {
                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(lowObstaclePrefab, lowSpawnPoint);
                }
                else
                {
                    Instantiate(highObstaclePrefab, highSpawnPoint);
                }
            }
            yield return new WaitForSeconds(spawnIntervalSeconds);
        }
    }
}
