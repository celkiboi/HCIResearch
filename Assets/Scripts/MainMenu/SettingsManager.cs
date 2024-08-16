using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static int FaceDetectionFrequency { get => faceDetectionFrequency; private set => faceDetectionFrequency = value; }
    public static int ColorDetectionPixelsToSkip { get => colorDetectionPixelsToSkip; private set => colorDetectionPixelsToSkip = value; }
    public static int FaceDetectionNeighbourCount { get => faceDetectionNeighbourCount; private set => faceDetectionNeighbourCount = value; }
    public static float FaceDetectionScale { get => faceDetectionScale; private set => faceDetectionScale = value; }

    [Settings("faceDetectionFrequency")]
    private static int faceDetectionFrequency = 18;
    [Settings("colorDetectionPixelsToSkip")]
    private static int colorDetectionPixelsToSkip = 1;
    [Settings("faceDetectionNeighbourCount")]
    private static int faceDetectionNeighbourCount = 4;
    [Settings("faceDetectionScale")]
    private static float faceDetectionScale = 1.3f;

    [SerializeField]
    TextMeshProUGUI faceDetectionFrequencyText;
    [SerializeField]
    Slider faceDetectionFrequencySlider;
    [SerializeField] 
    Slider faceDetectionNeighbourCountSlider;
    [SerializeField]
    TextMeshProUGUI faceDetectionNeighbourCountText;
    [SerializeField] 
    Slider faceDetectionScaleSlider;
    [SerializeField]
    TextMeshProUGUI faceDetectionScaleText;

    [SerializeField]
    TextMeshProUGUI colorDetectionPixelsText;
    [SerializeField] 
    Scrollbar colorDetectionPixelsScrollbar;

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

    [SerializeField]
    Slider movementSampleAmountSlider;
    [SerializeField]
    TextMeshProUGUI movementSampleAmountText;
    [SerializeField]
    Slider movementThresholdSlider;
    [SerializeField]
    TextMeshProUGUI movementThresholdText;
    [SerializeField]
    Slider movementErrorThresholdSlider;
    [SerializeField]
    TextMeshProUGUI movementErrorThresholdText;
    [SerializeField]
    Slider movementErrorSampleAmountSlider;
    [SerializeField]
    TextMeshProUGUI movementErrorSampleAmountText;



    public void OnFaceDetectionFrequencyChanged(float value)
    {
        FaceDetectionFrequency = (int)value;
        faceDetectionFrequencyText.text = FaceDetectionFrequency.ToString() + " Hz";
    }

    public void OnColorDetectionPixelsChanged(float value) 
    {
        // user may only select values, 1, 2, 4, 8, 16
        int parsedValue = (int)(value * 4);
        parsedValue = (int)Mathf.Pow(2, parsedValue);
        ColorDetectionPixelsToSkip = parsedValue;
        colorDetectionPixelsText.text = ColorDetectionPixelsToSkip.ToString() + " Pixels";
    }

    public void OnFaceDetectionNeighbourCountChanged(float value)
    {
        FaceDetectionNeighbourCount = (int)value;
        faceDetectionNeighbourCountText.text = FaceDetectionNeighbourCount.ToString();
    }

    public void OnFaceDetectionScaleChanged(float value)
    {
        FaceDetectionScale = value;
        faceDetectionScaleText.text = FaceDetectionScale.ToString();
    }

    public void OnMovementSampleAmountChanged(float value)
    {
        FaceMovementController.Instance.HeightSampleAmount = (int)value;
        movementSampleAmountText.text = FaceMovementController.Instance.HeightSampleAmount.ToString();
    }

    public void OnMovementThresholdChanged(float value)
    {
        FaceMovementController.Instance.HeightDifferenceThreshold = (int)value;
        movementThresholdText.text = FaceMovementController.Instance.HeightDifferenceThreshold.ToString();
    }

    public void OnMovementErrorThresholdChanged(float value)
    {
        FaceMovementController.Instance.OutlierThreshold = (int)value;
        movementErrorThresholdText.text = FaceMovementController.Instance.OutlierThreshold.ToString();
    }

    public void OnMovementErrorSampleAmountChanged(float value)
    {
        FaceMovementController.Instance.OutlierSampleCount = (int)value;
        movementErrorSampleAmountText.text = FaceMovementController.Instance.OutlierSampleCount.ToString();
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

    private void Start()
    {
        colorToleranceRedText.text = ColorDetector.GetColorTolerance().x.ToString();
        colorToleranceGreenText.text = ColorDetector.GetColorTolerance().y.ToString();
        colorToleranceBlueText.text = ColorDetector.GetColorTolerance().z.ToString();

        colorToleranceSliderRed.value = ColorDetector.GetColorTolerance().x;
        colorToleranceSliderGreen.value = ColorDetector.GetColorTolerance().y;
        colorToleranceSliderBlue.value = ColorDetector.GetColorTolerance().z;

        faceDetectionFrequencyText.text = FaceDetectionFrequency.ToString() + " Hz";
        faceDetectionFrequencySlider.value = FaceDetectionFrequency;

        colorDetectionPixelsScrollbar.value = (ColorDetectionPixelsToSkip - 1) / 16;
        colorDetectionPixelsText.text = ColorDetectionPixelsToSkip.ToString() + " Pixels";

        faceDetectionNeighbourCountText.text = FaceDetectionNeighbourCount.ToString();
        faceDetectionNeighbourCountSlider.value = FaceDetectionNeighbourCount;

        faceDetectionScaleText.text = FaceDetectionScale.ToString();
        faceDetectionScaleSlider.value = FaceDetectionScale;

        movementSampleAmountText.text = FaceMovementController.Instance.HeightSampleAmount.ToString();
        movementSampleAmountSlider.value = FaceMovementController.Instance.HeightSampleAmount;
        movementThresholdText.text = FaceMovementController.Instance.HeightDifferenceThreshold.ToString();
        movementThresholdSlider.value = FaceMovementController.Instance.HeightDifferenceThreshold;
        movementErrorSampleAmountText.text = FaceMovementController.Instance.OutlierSampleCount.ToString();
        movementErrorSampleAmountSlider.value = FaceMovementController.Instance.OutlierSampleCount;
        movementErrorThresholdText.text = FaceMovementController.Instance.OutlierThreshold.ToString();
        movementErrorThresholdSlider.value = FaceMovementController.Instance.OutlierThreshold;
    }
}
