using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CoinPowerUp : MonoBehaviour, IPowerUp
{
    public GameObject Player { get; set; }

    public string Text => $"+ {scoreModifier}";

    [SerializeField]
    int scoreModifier;

    public void Activate()
    {
        StartCoroutine(UsePowerup(HUDUpdater.PowerUpFlashDuration));   
    }

    IEnumerator UsePowerup(float duration)
    {
        HidePowerUp();
        NotifyHUD();
        ApplyEffect();

        yield return new WaitForSeconds(duration);

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
        GameManager.Score += scoreModifier;
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
