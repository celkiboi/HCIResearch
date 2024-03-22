using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDUpdater : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI score;
    [SerializeField]
    TextMeshProUGUI gameOver;
    [SerializeField]
    TextMeshProUGUI yourScore;
    [SerializeField]
    TextMeshProUGUI powerUpInfo;

    public static float PowerUpFlashDuration { get; } = 0.75f;

    public static string PowerUpText { get; set; } = "{PowerUpText}";

    void Start()
    {
        gameOver.gameObject.SetActive(false);
        yourScore.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        powerUpInfo.gameObject.SetActive(GameManager.IsPowerUpActive);
        if(GameManager.IsPowerUpActive ) 
        {
            powerUpInfo.text = PowerUpText;
        }
        if (GameManager.IsFinished) 
        {
            gameOver.gameObject.SetActive(true);
            yourScore.gameObject.SetActive(true);

            yourScore.text = $"Your score: {GameManager.Score}";

            score.gameObject.SetActive(false);
        }
        else
        {
            score.text = $"Score: {GameManager.Score}";
        }
    }

    public void UpdatePowerUpText(IPowerUp powerUp)
    {
        powerUpInfo.text = powerUp.Text;
    }
}
