using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreMenuManager : MonoBehaviour
{
    ScoreType currentlyDisplayedScoreType = ScoreType.keyboard;

    [SerializeField]
    TextMeshProUGUI highScoreText;
    [SerializeField]
    TextMeshProUGUI currentlyDisplayedScoreTypeText;
    [SerializeField]
    TextMeshProUGUI scoreMathStats;

    private void Start()
    {
        UpdateDisplayedText();
    }

    public void NextScoreType()
    {
        if (currentlyDisplayedScoreType == ScoreType.faceMovement)
            currentlyDisplayedScoreType = ScoreType.keyboard;
        else
            currentlyDisplayedScoreType++;
        UpdateDisplayedText();
    }

    public void PreviousScoreType()
    {
        if (currentlyDisplayedScoreType == ScoreType.keyboard)
            currentlyDisplayedScoreType = ScoreType.faceMovement;
        else
            currentlyDisplayedScoreType--;
        UpdateDisplayedText();
    }

    void UpdateDisplayedText()
    {
        currentlyDisplayedScoreTypeText.text = $"Score info for {currentlyDisplayedScoreType} method";

        IList<int> scores = GetScore();
        if (scores.Count == 0)
        {
            highScoreText.text = "No scores :(\nGo play and scores will be shown here!";
            scoreMathStats.text = "";
            return;
        }

        scores = scores.OrderByDescending(x => x).ToList();
        int numberOfScoresToDisplay = 3;
        int scoreDisplayedCounter = 0;
        highScoreText.text = "Best scores:\n";
        foreach (int score in scores) 
        {
            scoreDisplayedCounter++;
            if (scoreDisplayedCounter > numberOfScoresToDisplay)
                break;
            highScoreText.text += $"{score}\n";
        }

        scoreMathStats.text = "Total score: ";
        scoreMathStats.text += $"{scores.Sum()}\n";
        scoreMathStats.text += "Games played: ";
        scoreMathStats.text += $"{scores.Count()}\n";
        scoreMathStats.text += "Average score: ";
        scoreMathStats.text += $"{Math.Round(scores.Average(), 2)}\n";
        scoreMathStats.text += "Median score: ";
        scoreMathStats.text += $"{Math.Round(scores.Median(), 2)}\n";
        scoreMathStats.text += "Score std dev: ";
        scoreMathStats.text += $"{Math.Round(scores.StdDev(), 2)}\n";
        
    }

    private IList<int> GetScore()
    {
        HighScores scores = HighScores.Instance;
        return currentlyDisplayedScoreType switch
        {
            ScoreType.keyboard => scores.KeyboardScores,
            ScoreType.controller => scores.ControllerScores,
            ScoreType.faceHeight => scores.FaceHeightScores,
            ScoreType.color => scores.ColorScores,
            ScoreType.faceMovement => scores.FaceMovementScores,
            _ => throw new ArgumentException("Currently displayed score value does not match any cases in enum"),
        };
    }
}

