using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static int FaceDetectionFrequency { get => faceDetectionFrequency; private set => faceDetectionFrequency = value; }
    public static int ColorDetectionPixelsToSkip { get => colorDetectionPixelsToSkip; private set => colorDetectionPixelsToSkip = value; }

    [Settings("faceDetectionFrequency")]
    private static int faceDetectionFrequency = 18;
    [Settings("colorDetectionPixelsToSkip")]
    private static int colorDetectionPixelsToSkip = 1;

    [SerializeField]
    TextMeshProUGUI faceDetectionFrequencyText;

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
