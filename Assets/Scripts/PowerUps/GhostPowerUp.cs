using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPowerUp : MonoBehaviour, IPowerUp
{
    [SerializeField]
    float duration = 1.0f;
    [SerializeField]
    Renderer playerRenderer;
    Color color;


    public string Text => $"Ghost mode ACTIVE!";

    public GameObject Player { get; set; }
    public HUDUpdater HUDUpdater { get; set; }
    public PowerupSpawner PowerupSpawner { get; set; }
    public GameManager GameManagerInstance { get; set; }

    PlayerBehaviour playerBehaviour;

    public void Activate()
    {
        playerBehaviour = Player.GetComponent<PlayerBehaviour>();
        playerRenderer = Player.GetComponent<Renderer>();
        StartCoroutine(UsePowerup(duration));
    }

    IEnumerator UsePowerup(float duration)
    {
        this.GetComponent<Renderer>().enabled = false;
        this.GetComponent<ProjectileBehaviour>().enabled = false;

        playerBehaviour.IsPowerUpActive = true;
        HUDUpdater.PowerUpText = Text;

        color = playerRenderer.material.color;
        Color transperent = color;
        transperent.a = 0.5f;
        playerRenderer.material.color = transperent;
        playerBehaviour.CanCollide = false;

        yield return new WaitForSeconds(duration);

        playerRenderer.material.color = color;
        playerBehaviour.CanCollide = true;

        PowerupSpawner.AllowedToSpawn = true;
        PowerupSpawner.LastPowerupEndScore = GameManager.Score;

        playerBehaviour.IsPowerUpActive = false;

        Destroy(this.gameObject);
    }
}
