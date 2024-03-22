using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
            Destroy(collision.gameObject);
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            PowerupSpawner.LastPowerupEndScore = GameManager.Score;
            PowerupSpawner.AllowedToSpawn = true;
        }
    }
}
