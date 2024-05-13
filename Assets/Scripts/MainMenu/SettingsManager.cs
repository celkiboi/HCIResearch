using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static int FaceDetectionFrequency { get; private set; } = 18;

    [SerializeField]
    TextMeshProUGUI faceDetectionFrequencyText;
    [SerializeField]
    TextMeshProUGUI jumpThresholdText;
    [SerializeField]
    TextMeshProUGUI duckThresholdText;

    public void OnFaceDetectionFrequencyChanged(float value)
    {
        FaceDetectionFrequency = 6 + (int)(value * 24);
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
        jumpThresholdText.text = FaceDetectionController.JumpThreshold.ToString();
        duckThresholdText.text = FaceDetectionController.DuckThreshold.ToString();
    }
}
