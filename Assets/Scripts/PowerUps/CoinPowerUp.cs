using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CoinPowerUp : MonoBehaviour, IPowerUp
{
    PlayerBehaviour player;

    public GameObject Player { get; set; }
    public HUDUpdater HUDUpdater { get; set; }
    public PowerupSpawner PowerupSpawner { get; set; }
    public GameManager GameManagerInstance { get; set; }

    public string Text => $"+ {scoreModifier}";

    [SerializeField]
    int scoreModifier;

    public void Activate()
    {
        player = Player.GetComponent<PlayerBehaviour>();
        StartCoroutine(UsePowerup(HUDUpdater.PowerUpFlashDuration));
    }

    IEnumerator UsePowerup(float duration)
    {
        this.GetComponent<Renderer>().enabled = false;
        this.GetComponent<ProjectileBehaviour>().enabled = false;

        player.IsPowerUpActive = true;
        HUDUpdater.PowerUpText = Text;

        GameManagerInstance.AddToScore(scoreModifier);

        yield return new WaitForSeconds(duration);

        PowerupSpawner.AllowedToSpawn = true;
        PowerupSpawner.LastPowerupEndScore = GameManager.Score;

        player.IsPowerUpActive = false;

        Destroy(this.gameObject);
    }
}
