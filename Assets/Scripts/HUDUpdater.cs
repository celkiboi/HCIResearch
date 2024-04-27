using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    Button playAgain;
    [SerializeField]
    TextMeshProUGUI countdownText;
    [SerializeField]
    Button mainMenu;
    [SerializeField]
    GameObject glideDown;

    [SerializeField]
    PlayerBehaviour player;

    public float PowerUpFlashDuration { get; } = 0.75f;

    public string PowerUpText { get; set; } = "{PowerUpText}";

    void Start()
    {
        gameOver.gameObject.SetActive(false);
        yourScore.gameObject.SetActive(false);
        playAgain.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        powerUpInfo.gameObject.SetActive(player.IsPowerUpActive);
        if(player.IsPowerUpActive ) 
        {
            powerUpInfo.text = PowerUpText;
        }
        if (GameManager.IsFinished) 
        {
            gameOver.gameObject.SetActive(true);
            yourScore.gameObject.SetActive(true);
            playAgain.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(true);

            yourScore.text = $"Your score: {GameManager.Score}";

            score.gameObject.SetActive(false);
        }
        else
        {
            score.text = $"Score: {GameManager.Score}";
        }
    }

    private void Update()
    {
        glideDown.SetActive(player.CanGlide);
    }

    public void UpdatePowerUpText(IPowerUp powerUp)
    {
        powerUpInfo.text = powerUp.Text;
    }

    public void UpdateCountDown(int secondsRemaining)
    {
        if (secondsRemaining <= 0)
        {
            countdownText.gameObject.SetActive(false);
        }
        countdownText.text = secondsRemaining.ToString();
    }
}
