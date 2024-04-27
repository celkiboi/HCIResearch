using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    [SerializeField]
    PowerupSpawner powerupSpawner;
    [SerializeField]
    ObstacleSpawner obstacleSpawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            obstacleSpawner.CanSpawn = true;
        }
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            powerupSpawner.LastPowerupEndScore = GameManager.Score;
            powerupSpawner.AllowedToSpawn = true;
        }
    }
}
