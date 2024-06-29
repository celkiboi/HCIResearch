using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static int FaceDetectionFrequency { get; private set; } = 18;
    public static int ColorDetectionPixelsToSkip { get; private set; } = 1;

    [SerializeField]
    TextMeshProUGUI faceDetectionFrequencyText;
    [SerializeField]
    TextMeshProUGUI jumpThresholdText;
    [SerializeField]
    TextMeshProUGUI duckThresholdText;

    [SerializeField]
    TextMeshProUGUI colorDetectionPixelsText;

    [SerializeField]
    TextMeshProUGUI colorToleranceRedText;
    [SerializeField]
    TextMeshProUGUI colorToleranceGreenText;
    [SerializeField]
    TextMeshProUGUI colorToleranceBlueText;
    [SerializeField]
    Slider colorToleranceSliderRed;
    [SerializeField]
    Slider colorToleranceSliderGreen;
    [SerializeField]
    Slider colorToleranceSliderBlue;

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
        FaceDetectionController.Instance.SetThreshold(
            duck: FaceDetectionController.Instance.DuckThreshold,
            jump: (int)value);
        ColorDetectionController.Instance.SetThreshold(
            duck: FaceDetectionController.Instance.DuckThreshold,
            jump: (int)value);
    }

    public void OnDuckThresholdChanged(float value) 
    {
        FaceDetectionController.Instance.SetThreshold(
            jump: FaceDetectionController.Instance.JumpThreshold,
            duck: (int)value);
        ColorDetectionController.Instance.SetThreshold(
            jump: FaceDetectionController.Instance.JumpThreshold,
            duck: (int)value);
    }

    public void OnColorToleranceRedChanged(float value)
    {
        ColorDetector.SetColorToleranceRed(value);
        colorToleranceRedText.text = value.ToString();
    }

    public void OnColorToleranceGreenChaned(float value)
    {
        ColorDetector.SetColorToleranceGreen(value);
        colorToleranceGreenText.text = value.ToString();
    }

    public void OnColorToleranceBlueChaned(float value)
    {
        ColorDetector.SetColorToleranceBlue(value);
        colorToleranceBlueText.text = value.ToString();

    }

    private void Update()
    {
        faceDetectionFrequencyText.text = FaceDetectionFrequency.ToString() + " Hz";
        colorDetectionPixelsText.text = ColorDetectionPixelsToSkip.ToString() + " Pixels";
        jumpThresholdText.text = FaceDetectionController.Instance.JumpThreshold.ToString();
        duckThresholdText.text = FaceDetectionController.Instance.DuckThreshold.ToString();
    }

    private void Start()
    {
        colorToleranceRedText.text = ColorDetector.GetColorTolerance().x.ToString();
        colorToleranceGreenText.text = ColorDetector.GetColorTolerance().y.ToString();
        colorToleranceBlueText.text = ColorDetector.GetColorTolerance().z.ToString();

        colorToleranceSliderRed.value = ColorDetector.GetColorTolerance().x;
        colorToleranceSliderGreen.value = ColorDetector.GetColorTolerance().y;
        colorToleranceSliderBlue.value = ColorDetector.GetColorTolerance().z;
    }
}
