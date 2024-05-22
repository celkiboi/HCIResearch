using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static int FaceDetectionFrequency { get; private set; } = 18;
    public static int ColorDetectionPixelsToSkip { get; private set; } = 4;

    [SerializeField]
    TextMeshProUGUI faceDetectionFrequencyText;
    [SerializeField]
    TextMeshProUGUI jumpThresholdText;
    [SerializeField]
    TextMeshProUGUI duckThresholdText;

    [SerializeField]
    TextMeshProUGUI colorDetectionPixelsText;

    public void OnFaceDetectionFrequencyChanged(float value)
    {
        FaceDetectionFrequency = 6 + (int)(value * 24);
    }

    public void OnColorDetectionPixelsChanged(float value) 
    {
        // user may only select values, 1, 2, 4, 8, 16
        int parsedValue = (int)(value * 4);
        parsedValue = (int)Mathf.Pow(2, parsedValue);
        ColorDetectionPixelsToSkip = parsedValue;
    }

    public void OnJumpThresholdChanged(float value)
    {
        FaceDetectionController.SetThreshold(
            duck: FaceDetectionController.DuckThreshold,
            jump: (int)value);
    }

    public void OnDuckThresholdChanged(float value) 
    {
        FaceDetectionController.SetThreshold(
            jump: FaceDetectionController.JumpThreshold,
            duck: (int)value);
    }

    private void Update()
    {
        faceDetectionFrequencyText.text = FaceDetectionFrequency.ToString() + " Hz";
        colorDetectionPixelsText.text = ColorDetectionPixelsToSkip.ToString() + " Pixels";
        jumpThresholdText.text = FaceDetectionController.JumpThreshold.ToString();
        duckThresholdText.text = FaceDetectionController.DuckThreshold.ToString();
    }
}
