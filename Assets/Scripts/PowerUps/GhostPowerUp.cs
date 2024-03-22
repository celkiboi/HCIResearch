using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPowerUp : MonoBehaviour, IPowerUp
{
    [SerializeField]
    float duration = 1.0f;
    Renderer playerRenderer;
    Color color;

    public GameObject Player { get; set; }

    public string Text => $"Ghost mode ACTIVE!";

    public void Activate()
    {
        playerRenderer = Player.GetComponent<Renderer>();
        StartCoroutine(UsePowerup(duration));
    }

    IEnumerator UsePowerup(float duration)
    {
        HidePowerUp();
        NotifyHUD();
        ApplyEffect();

        yield return new WaitForSeconds(duration);

        RevertEffect();
        NotifyPowerUpSpawner();
        RevertHUD();

        Destroy(this.gameObject);
    }

    void HidePowerUp()
    {
        this.GetComponent<Renderer>().enabled = false;
        this.GetComponent<ProjectileBehaviour>().enabled = false;
    }

    void NotifyHUD()
    {
        GameManager.IsPowerUpActive = true;
        HUDUpdater.PowerUpText = Text;
    }

    void ApplyEffect()
    {
        color = playerRenderer.material.color;
        Color transperent = color;
        transperent.a = 0.5f;
        playerRenderer.material.color = transperent;
        PlayerBehaviour.CanCollide = false;
    }

    void RevertEffect()
    {
        playerRenderer.material.color = color;
        PlayerBehaviour.CanCollide = true;
    }
    private void RevertHUD()
    {
        GameManager.IsPowerUpActive = false;
    }

    void NotifyPowerUpSpawner()
    {
        PowerupSpawner.AllowedToSpawn = true;
        PowerupSpawner.LastPowerupEndScore = GameManager.Score;
    }
}
