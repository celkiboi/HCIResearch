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

    private void Start()
    {
        StartCoroutine(SpawnEnemiesInIntervals(spawnIntervalSeconds));
    }

    IEnumerator SpawnEnemiesInIntervals(float spawnIntervalSeconds)
    {
        for (;;)
        {
            yield return new WaitForSeconds(spawnIntervalSeconds);
            if (Random.Range(0, 2) == 0)
            {
                Instantiate(lowObstaclePrefab, lowSpawnPoint);
            }
            else
            {
                Instantiate(highObstaclePrefab, highSpawnPoint);
            }
        }
    }
}
